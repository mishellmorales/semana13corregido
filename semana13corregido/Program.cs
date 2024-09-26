using System;

class Revista
{
    public string Titulo { get; set; }
    public Revista Izquierda { get; set; }
    public Revista Derecha { get; set; }

    public Revista(string titulo)
    {
        Titulo = titulo;
        Izquierda = null;
        Derecha = null;
    }
}

class Catalogo
{
    public Revista Raiz { get; private set; }

    public void Insertar(string titulo)
    {
        Raiz = InsertarIterativo(Raiz, titulo);
    }

    private Revista InsertarIterativo(Revista nodo, string titulo)
    {
        Revista nuevoNodo = new Revista(titulo);
        if (nodo == null)
        {
            return nuevoNodo;
        }

        Revista actual = nodo;
        Revista padre = null;

        while (actual != null)
        {
            padre = actual;
            if (string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase) < 0)
            {
                actual = actual.Izquierda;
            }
            else if (string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase) > 0)
            {
                actual = actual.Derecha;
            }
            else
            {
                // Ya existe, no hacer nada
                return nodo;
            }
        }

        if (string.Compare(titulo, padre.Titulo, StringComparison.OrdinalIgnoreCase) < 0)
        {
            padre.Izquierda = nuevoNodo;
        }
        else
        {
            padre.Derecha = nuevoNodo;
        }

        return nodo;
    }

    public bool Buscar(string titulo)
    {
        return BuscarIterativo(Raiz, titulo);
    }

    private bool BuscarIterativo(Revista nodo, string titulo)
    {
        while (nodo != null)
        {
            if (titulo.Equals(nodo.Titulo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Compare(titulo, nodo.Titulo, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nodo = nodo.Izquierda;
            }
            else
            {
                nodo = nodo.Derecha;
            }
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Catalogo catalogo = new Catalogo();

        // Ingresar 10 títulos de revistas
        string[] titulos = { "Revista1", "Revista2", "Revista3", "Revista4", "Revista5",
                             "Revista6", "Revista7", "Revista8", "Revista9", "Revista10" };

        foreach (var titulo in titulos)
        {
            catalogo.Insertar(titulo);
        }

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Buscar un título");
            Console.WriteLine("2. Salir");
            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el título a buscar: ");
                    string tituloABuscar = Console.ReadLine();
                    bool encontrado = catalogo.Buscar(tituloABuscar);
                    Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
                    break;
                case 2:
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}

