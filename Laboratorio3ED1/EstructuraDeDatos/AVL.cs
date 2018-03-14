using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
    public class AVL<T> : IAVL<T>  where T :IComparable
    {

        public Nodo<T> root;

        public AVL()
        {
            root = null;
        }
        public Nodo<T> Buscar(T value)
        {
            var auxiliar = root;
            while (auxiliar.value.CompareTo(value) != 0)
            {
                if (value.CompareTo(auxiliar.value) < 0)
                {
                    auxiliar = auxiliar.Izquierdo;
                }
                else
                {
                    auxiliar = auxiliar.Derecho;
                }
                if (auxiliar == null)
                {
                    return null;
                }
            }
            return auxiliar;
        }

        public Nodo<T> Eliminar(T value)
        {
            throw new NotImplementedException();
        }

        

        public void Insertar(T value)
        {
            var newnode = new Nodo<T>(value);
            if(root == null)
            {
                root = newnode;
            } else
            {
                InsertarInterno(newnode, root);
            }

        }

        private void InsertarInterno(Nodo<T> newnode, Nodo<T> padre)
        {
            if (padre != null)
            {
                if (newnode.value.CompareTo(padre.value) <= 0)
                {
                    if (padre.Izquierdo == null)
                    {
                        
                        padre.Izquierdo = newnode;
                        padre.Izquierdo.Padre = padre;
                    }
                    else
                    {
                        InsertarInterno(newnode, padre.Izquierdo);
                    }
                }
                else
                {
                    if (newnode.value.CompareTo(padre.value) > 0)
                    {
                        if (padre.Derecho == null)
                        {
                            
                            padre.Derecho = newnode;
                            padre.Derecho.Padre = padre;
                        }
                        else
                        {
                            InsertarInterno(newnode, padre.Derecho);
                        }
                    }
                }
            }

        }
        private void Balancear(Nodo<T> node , bool isnew, bool isleft)
        {
            bool exit = false;

           while((node != null) && !exit)
            {
                bool didRotate = false;

                if(isnew)
                {
                    if (isleft)
                    {
                        node.Factor--;
                    } else
                    {
                        node.Factor++;
                    }
                }
                else
                {
                    if(node.Factor == 0)
                    {
                        exit = true;
                    }
                    if(isleft)
                    {
                        node.Factor++;
                    }
                     else
                    {
                        node.Factor--;
                    }

                }
                if(node.Factor == 0)
                {
                    exit = true;
                }
                else if(node.Factor == -2)
                {
                    if(node.Izquierdo.Factor == 1)
                    {
                        didRotate = true;
                    } else
                    {
                        didRotate = true;
                    }
                    exit = true;
                }

            }
        }

        public void PostFijo(Recorrido<T> Path)
        {
            throw new NotImplementedException();
        }

        public void Prefijo(Recorrido<T> Path)
        {
            throw new NotImplementedException();
        }
        public void Infijo(Recorrido<T> Path)
        {
            throw new NotImplementedException();
        }
    }
}
