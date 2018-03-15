using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
    public delegate void Recorrido<T>(Nodo<T> node);
    public delegate int CompareTo<T>(T value, T value2);
    public interface IAVL<T>
    {
        
        void Insertar(T value);
        Nodo<T> Eliminar(T value);
        Nodo<T> Buscar(T value);
        void Infijo(Recorrido<T> Path);
        void Prefijo(Recorrido<T> Path);
        void PostFijo(Recorrido<T> Path);

    }
}
