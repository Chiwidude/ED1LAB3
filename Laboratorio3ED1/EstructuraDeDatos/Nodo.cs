using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
   public class Nodo<T>
    {
        public Nodo<T> Derecho;

        public Nodo<T> Izquierdo;

        public Nodo<T> padre;

        public T value;

        int factor;
        /// <summary>
        /// Constructor Nodo AVL
        /// </summary>
        /// <param name="value"></param> Valor a almacenar
        /// <param name="derecho"></param>Referencia Nodo derecho
        /// <param name="izauierdo"></param> Referencia Nodo Izquierdo
        /// <param name="padre"></param>Referencia al Padre del nodo

        public Nodo(T value, Nodo<T> derecho, Nodo<T> izquierdo)
        {
            this.value = value;
            this.Derecho = derecho;
            this.Izquierdo = izquierdo;
            this.padre = null;
            this.factor = 0;
                
        }

        public Nodo(T value) : this(value, null, null) { }
        

        public int Factor
        {
            get
            {
                return factor;
            }
            set
            {
                factor = value;
            }
        }

        public Nodo<T> Padre
        {
            get
            {
                return padre;
            }
            set
            {
                this.padre = value;
            }
        }
        
    }
}
