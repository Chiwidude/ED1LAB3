using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{

    public delegate int CompareTo<T>(T value, T value2);
    public interface IAVL<T>
    {
        
        void Insertar(T value);
        Nodo<T> Eliminar(T value);
        Nodo<T> Buscar(T value);
        List<T> Infijo();
        List<T> Prefijo();
        List<T> PostFijo();

    }
}
