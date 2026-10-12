using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caso_EMPLEADO
{
    internal class Program
    {

        static int max_emp = 50;
        static string[] nombres_emp = new string[max_emp];
        static string[] correo_emp = new string[max_emp];
        static string ruta_archivo = "empleados.txt";
        static int con_emp = 0;

        static public void titulo()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("             EMPLOYED SISTEMS              ");
            Console.WriteLine("-------------------------------------------------");
        }
        static public void registrar_empleado()
        {
            Console.WriteLine("****REGISTRO DE EMPLEADO****");
            if (con_emp >= max_emp)
            {
                Console.WriteLine("ERROR 001: CAPACIDAD MAXIMA ALCANZADA.");
                return;
            }
            Console.Write("Ingrese el nombre del empleado: ");
            string nombres = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres))

            {
                Console.WriteLine("ERROR 002: EL CAMPO NOMBRES NO PUEDE ESTAR VACIO.");
                return;
            }
            string nombre_Mayus = nombres.ToUpper();
            string[] partes = nombre_Mayus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "USUARIO";
            string correo = $"{primer_nombre.ToLower()}@upn.pe";
            nombres_emp[con_emp] = nombre_Mayus;
            correo_emp[con_emp] = correo;
            con_emp++;
        }

        static public void buscar_empleado()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("****BUSCAR EMPLEADO****");
            Console.WriteLine("-------------------------------------------------");
            if (con_emp == 0)
            {
                Console.WriteLine("ERROR 003: NO HAY EMPLEADOS REGISTRADOS.");
                return;
            }
            Console.Write("Ingrese el nombre del empleado a buscar: ");
            string nombre_buscar = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(nombre_buscar))
                return;
            bool encontrado = false;
            for (int i = 0; i < con_emp; i++)
            {
                if (nombres_emp[i].Contains(nombre_buscar))
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine($"Nombre: {nombres_emp[i]}");    //julianjose
                    Console.WriteLine($"Correo: {correo_emp[i]}");
                    Console.WriteLine("-------------------------------------------------");
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("ERROR 004: EMPLEADO NO ENCONTRADO.");
            }
        }

        static public void orden_alfa()
        {
            if (con_emp == 0)
            {
                Console.WriteLine("ERROR 005: NO HAY EMPLEADOS REGISTRADOS.");
                return;
            }
            for (int i = 0; i < con_emp - 1; i++)
            {
                for (int j = i + 1; j < con_emp; j++)
                {
                    if (string.Compare(nombres_emp[i], nombres_emp[j]) > 0)
                    {
                        string tempNombre = nombres_emp[i];  //julianjose
                        string tempCorreo = correo_emp[i];
                        nombres_emp[i] = nombres_emp[j];
                        correo_emp[i] = correo_emp[j];
                        nombres_emp[j] = tempNombre;
                        correo_emp[j] = tempCorreo;
                    }
                }
            }
        }

        static public void mostrar()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("****LISTA DE EMPLEADOS****");
            Console.WriteLine("-------------------------------------------------");
            if (con_emp == 0)
            {
                Console.WriteLine("ERROR 006: NO HAY EMPLEADOS REGISTRADOS.");
                return;
            }
            Console.WriteLine("NOMBRE Y APELLIDO \t\t CORREO CORPORATIVO");
            for (int i = 0; i < con_emp; i++)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Nombre: {nombres_emp[i]}");
                Console.WriteLine($"Correo: {correo_emp[i]}");
                Console.WriteLine("-------------------------------------------------");
            }
        }
        static public void guardar_datos()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("****GUARDAR DATOS EN ARCHIVO****");
            Console.WriteLine("-------------------------------------------------");
            if (con_emp == 0)
            {
                Console.WriteLine("ERROR 009: NO HAY EMPLEADOS REGISTRADOS.");
                return;
            }
            using (System.IO.StreamWriter escribir = new System.IO.StreamWriter(ruta_archivo))
            {
                for (int i = 0; i < con_emp; i++)
                {
                    escribir.WriteLine($"{nombres_emp[i]},{correo_emp[i]}");
                }
            }
            Console.WriteLine(con_emp + " empleados guardados en el archivo " + ruta_archivo);
        }
        static public void leer_datos()
        {
            Console.WriteLine("*******LEER DATOS DESDE ARCHIVO*******");
            Console.WriteLine("*************************************");
            if (!System.IO.File.Exists(ruta_archivo))
            {
                Console.WriteLine($"El archivo '{ruta_archivo}' no existe.");
                return;
            }
            string[] lineas = System.IO.File.ReadAllLines(ruta_archivo);
            if (lineas.Length == 0)
            {
                Console.WriteLine("El archivo está vacío.");
                return;
            }
            Console.WriteLine("Nombres y Apellidos\t\tCorreo corporativo");
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');
                if (datos.Length == 2)
                {
                    Console.WriteLine($"Empleado: {datos[0]} - Correo: {datos[1]}");
                }

            }

        }


        static public void cargar_datos()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("****CARGAR DATOS DESDE ARCHIVO****");
            Console.WriteLine("-------------------------------------------------");
            if (!File.Exists(ruta_archivo))
            {
                Console.WriteLine("ERROR 010: EL ARCHIVO NO EXISTE.");
                return;
            }
            using (System.IO.StreamReader leer = new System.IO.StreamReader(ruta_archivo))
            {
                string linea;
                con_emp = 0;
                while ((linea = leer.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');
                    if (datos.Length == 2)
                    {
                        nombres_emp[con_emp] = datos[0];
                        correo_emp[con_emp] = datos[1];
                        con_emp++;
                    }
                }
            }
            Console.WriteLine(con_emp + " empleados cargados desde el archivo " + ruta_archivo);
        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 8)
            {
                Console.Clear();
                titulo();
                Console.WriteLine("[1] Registrar empleado");
                Console.WriteLine("[2] Buscar empleado");
                Console.WriteLine("[3] Ordenar empleados alfabéticamente");
                Console.WriteLine("[4] Mostrar empleados");
                Console.WriteLine("[5] Guardar datos en archivo");
                Console.WriteLine("[6] Cargar datos desde archivo");
                Console.WriteLine("[7] leer datos desde archivo");
                Console.WriteLine("[8] Salir");
                Console.Write("Ingrese una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("ERROR 007: OPCIÓN INVÁLIDA. DEBE INGRESAR UN NÚMERO.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        registrar_empleado();
                        break;
                    case 2:
                        buscar_empleado();
                        break;
                    case 3:
                        orden_alfa();
                        break;
                    case 4:
                        mostrar();
                        break;
                    case 5:
                        guardar_datos();
                        break;
                    case 6:
                        cargar_datos();
                        break;
                    case 7:
                        leer_datos();
                        break;
                    case 8:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("ERROR 008: OPCIÓN INVÁLIDA. DEBE INGRESAR UN NÚMERO ENTRE 1 Y 5.");
                        break;
                }
                Console.ReadKey();

            }

        }
    }
}
