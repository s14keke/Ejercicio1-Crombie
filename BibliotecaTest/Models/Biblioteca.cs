using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaTest
{
    public class Biblioteca
    {
        private List<Libro> libros = new List<Libro>();
        private List<Usuario> usuarios = new List<Usuario>();

        public void AgregarLibro()
        {
            Console.Write("Ingrese título del libro: ");
            string titulo = Console.ReadLine();
            Console.Write("Ingrese autor del libro: ");
            string autor = Console.ReadLine();
            Console.Write("Ingrese ISBN del libro: ");
            string isbn = Console.ReadLine();

            libros.Add(new Libro(titulo, autor, isbn));
            Console.WriteLine("Libro agregado exitosamente.");
        }

        public void RegistrarUsuario()
        {
            Console.Write("Ingrese nombre del usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese ID del usuario: ");
            string id = Console.ReadLine();

            usuarios.Add(new Usuario(nombre, id));
            Console.WriteLine("Usuario registrado exitosamente.");
        }

        public void PrestarLibro()
        {
            Console.Write("Ingrese ISBN del libro: ");
            string isbn = Console.ReadLine();
            Console.Write("Ingrese ID del usuario: ");
            string idUsuario = Console.ReadLine();

            var libro = libros.FirstOrDefault(l => l.ISBN == isbn && l.Disponible);
            var usuario = usuarios.FirstOrDefault(u => u.ID == idUsuario);

            if (libro != null && usuario != null)
            {
                libro.Disponible = false;
                usuario.LibrosPrestados.Add(libro);
                Console.WriteLine("Libro prestado exitosamente.");
            }
            else
            {
                Console.WriteLine("Error: El libro no está disponible o el usuario no existe.");
            }
        }

        public void DevolverLibro()
        {
            Console.Write("Ingrese ISBN del libro: ");
            string isbn = Console.ReadLine();
            Console.Write("Ingrese ID del usuario: ");
            string idUsuario = Console.ReadLine();

            var usuario = usuarios.FirstOrDefault(u => u.ID == idUsuario);
            if (usuario != null)
            {
                var libro = usuario.LibrosPrestados.FirstOrDefault(l => l.ISBN == isbn);
                if (libro != null)
                {
                    libro.Disponible = true;
                    usuario.LibrosPrestados.Remove(libro);
                    Console.WriteLine("Libro devuelto exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error: El usuario no tiene este libro.");
                }
            }
            else
            {
                Console.WriteLine("Error: Usuario no encontrado.");
            }
        }

        public void VerEstadoLibros()
        {
            Console.WriteLine("\nLista de todos los libros:");
            foreach (var libro in libros)
            {
                string estado = libro.Disponible ? "Disponible" : "Prestado";
                Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}, Estado: {estado}");
            }
        }

        public void VerLibrosPrestadosDeUsuario()
        {
            Console.Write("Ingrese ID del usuario: ");
            string idUsuario = Console.ReadLine();

            var usuario = usuarios.FirstOrDefault(u => u.ID == idUsuario);
            if (usuario != null)
            {
                Console.WriteLine($"\nLibros prestados por {usuario.Nombre}:");
                if (usuario.LibrosPrestados.Count == 0)
                {
                    Console.WriteLine("No tiene libros prestados.");
                }
                else
                {
                    foreach (var libro in usuario.LibrosPrestados)
                    {
                        Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Usuario no encontrado.");
            }
        }
    }
}
