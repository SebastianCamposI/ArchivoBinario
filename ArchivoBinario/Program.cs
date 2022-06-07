using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivoBinario
{
    class ArchivosBinariosEmpleados
    {
        //declaración de flujos
        BinaryWriter bw = null; //Flujo de salida - escritura de datos
        BinaryReader br = null; //Flujo de entrada - lectura de datos

        //Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //Variable local método
            char resp;

            try 
            {
                //Creación del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                do
                {
                    Console.Clear();
                    Console.WriteLine("Número del empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Nombre del empleado: ");
                    Nombre = Console.ReadLine();
                    Console.WriteLine("Dirección del empleado: ");
                    Direccion = Console.ReadLine();
                    Console.WriteLine("Teléfono del empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.WriteLine("Dias trabajados del empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Salario diario del empleado: ");
                    SalarioDiario = Int32.Parse(Console.ReadLine());

                    //Escribe los datos al archivo

                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.WriteLine("\n\nDeseas almacenar otro registro (s/n)?");
                    resp = Char.Parse(Console.ReadLine());

                } while ((resp == 's') || (resp == 'S'));
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta: " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); //Cierra el flujo - escritura
                Console.WriteLine("\nPresione ENTER para terminar la escritura de datos y regresar al menú");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //Verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                    //Despliegue de datos
                    Console.Clear();
                    do
                    {
                        //Lectura de registros miesntras no llegeu a EnfOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //Muestra los datos
                        Console.WriteLine("Número del empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Dirección del empleado: " + Direccion);
                        Console.WriteLine("Teléfono del empleado: " + Telefono);
                        Console.WriteLine("Dias trabajados del empleado: " + DiasTrabajados);
                        Console.WriteLine("Salario diario del empleado: {0:C}", SalarioDiario);
                        Console.WriteLine("Sueldo total del empleado: {0:C}", (DiasTrabajados * SalarioDiario));
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl archivo " + Archivo + " No existe en el disco!!");
                    Console.Write("\nPresione ENTER para continuar...");
                    Console.ReadKey();
                } 
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado de empleados");
                Console.Write("Presione ENTER para continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close();//cierra el flujo
                Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menu");
                Console.ReadKey();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //declaracion de variables auxiliares
            string Arch = null;
            int opcion;

            //creacion de objeto
            ArchivosBinariosEmpleados Al = new ArchivosBinariosEmpleados();
            do
            {
                Console.Clear();
                Console.WriteLine("\n***ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1 Creacion del archivo");
                Console.WriteLine("2 Lectura de archivo");
                Console.WriteLine("3 Salida del programa");
                Console.Write("\nQue opción desea: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura de archivo 
                            Console.Write("\nAlimenta el nombre del archivo a crear: "); Arch = Console.ReadLine();

                            //verifica si existe el archivo
                            char reap = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo Existe!!, deseas sobreescribirlo (s/n)?");
                                reap = char.Parse(Console.ReadLine());
                            }
                            if ((reap == 's') || (reap == 'S'))
                            {
                                Al.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura del nombre archivo
                            Console.Write("\nAlimenta el nombre del archivo que desea leer: "); Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);

                        }
                        break;
                    case 3:
                        Console.Write("\nPresione ENTER para salir del programa");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa opcion no existe!!, Presione ENTER para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}
