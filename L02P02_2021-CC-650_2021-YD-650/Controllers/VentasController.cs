using L02P02_2021_CC_650_2021_YD_650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace L02P02_2021_CC_650_2021_YD_650.Controllers
{
    public class VentasController : Controller
    {
        public static PedidoEncabezado Encabezado;
        public static Cliente Cliente;

        private readonly LibreriaDbContext _libreriaDbContext;

        public VentasController(LibreriaDbContext libreriaDbContext)
        {
            _libreriaDbContext = libreriaDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IniciarVentas(Cliente cliente)
        {
            try
            {
                _libreriaDbContext.Add(cliente);
                _libreriaDbContext.SaveChanges();
                int idCliente = _libreriaDbContext.Clientes.Select(c => (int?)c.Id).Max() ?? 0; // Obtener id de cliente
                Encabezado = new PedidoEncabezado();
                Encabezado.IdCliente = idCliente;
                Encabezado.Total = 0;
                Encabezado.CantidadLibros = 0;
                Encabezado.estado = "P";
                _libreriaDbContext.Add(Encabezado);
                _libreriaDbContext.SaveChanges();
                int idEncabezado = _libreriaDbContext.PedidoEncabezados.Select(c => (int?)c.Id).Max() ?? 0; // Obtener el id del encabezado
                Encabezado = _libreriaDbContext.PedidoEncabezados.FirstOrDefault(e => e.Id == idEncabezado);
                Cliente = _libreriaDbContext.Clientes.FirstOrDefault(e => e.Id == idCliente);
                Debug.WriteLine(Encabezado.Id + Encabezado.estado + Encabezado.IdCliente);
                Debug.WriteLine(Cliente.Id + Cliente.Nombre);
                return RedirectToAction("ListadoLibro");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult MandarFin()
        {
            return RedirectToAction("confirmVenta");
        }

        public IActionResult ListadoLibros()
        {
            if (Encabezado == null || Cliente == null)
            {
                return RedirectToAction("Index");
            }
            var listadoLibros = (from e in _libreriaDbContext.Libros
                               join a in _libreriaDbContext.Autores on e.IdAutor equals a.Id
                               select new
                               {
                                   id = e.Id,
                                   nombre = e.Nombre,
                                   autor = a.Autor,
                                   precio = e.Precio
                               }).ToList();
            ViewData["listadoLibros"] = listadoLibros;
            ViewBag.cabeza = Encabezado;
            return View();
        }

        public IActionResult confirmarVenta()
        {
            if (Encabezado == null || Cliente == null)
            {
                return RedirectToAction("Index");
            }
            var listaCarrito = (from e in _libreriaDbContext.PedidoDetalles
                              join cab in _libreriaDbContext.PedidoEncabezados on e.IdPedido equals cab.Id
                              join l in _libreriaDbContext.Libros on e.IdLibro equals l.Id
                              join a in _libreriaDbContext.Autores on l.IdAutor equals a.Id
                              where cab.Id == Encabezado.Id
                              select new
                              {
                                  nombre = l.Nombre,
                                  autor = a.Autor,
                                  precio = l.Precio
                              }).ToList();
            ViewData["listadodeCarrito"] = listaCarrito;
            ViewBag.cliente = Cliente;
            ViewBag.cabezon = Encabezado;
            return View();
        }

        public IActionResult Agregar(int id, decimal precio)
        {
            PedidoDetalle pedidoDetalle = new PedidoDetalle();
            pedidoDetalle.IdLibro = id;
            pedidoDetalle.IdPedido = Encabezado.Id;
            _libreriaDbContext.Add(pedidoDetalle);
            _libreriaDbContext.SaveChanges();
            PedidoEncabezado alterPed = _libreriaDbContext.PedidoEncabezados.FirstOrDefault(e => e.Id == Encabezado.Id);
            if (alterPed != null)
            {
                alterPed.Total += precio;
                alterPed.CantidadLibros++;
                Encabezado = alterPed;
                _libreriaDbContext.Entry(alterPed).State = EntityState.Modified;
                _libreriaDbContext.SaveChanges();
            }
            return RedirectToAction("ListaLibros");
        }

        public IActionResult Finalizar()
        {
            PedidoEncabezado alterPedi = _libreriaDbContext.PedidoEncabezados.FirstOrDefault(e => e.Id == Encabezado.Id);
            if (alterPedi != null)
            {
                alterPedi.estado = "C";
                _libreriaDbContext.Entry(alterPedi).State = EntityState.Modified;
                _libreriaDbContext.SaveChanges();
            }
            Encabezado = null;
            Cliente = null;
            TempData["Mensaje"] = "Compra realizada Correctamente!!!";
            return RedirectToAction("Index");
        }
    }
}