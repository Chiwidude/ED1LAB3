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
                        RotaciónDerecha(node);
                        didRotate = true;
                    }
                    exit = true;
                } else if( node.Factor == 2)
                {
                    if(node.Derecho.Factor == -1)
                    {
                        didRotate = true;
                    }
                    else
                    {
                        RotaciónIzquierda(node);
                        didRotate = true;
                    }
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

        protected void RotaciónDerecha(Nodo<T> nodo)
        {
            var Rootparent = nodo.Padre as Nodo<T>;
            var Q = nodo as Nodo<T>;
            var P = Q.Izquierdo as Nodo<T>;
            var C = P.Derecho as Nodo<T>;

            if (nodo.Padre is Nodo<T> RootParent)
            {
                if (RootParent.Derecho == Q)
                {
                    RootParent.Derecho = P;
                }
                else
                {
                    RootParent.Izquierdo = P;
                }

            }
            else
            {
                root = P as Nodo<T>;
                root.Padre = null;
            }

            Q.Izquierdo = C;
            P.Derecho = Q;
            Q.Padre = P;

            if(C != null)
                C.Padre = Q;
                P.Padre = Rootparent;
            

            if(P.Factor == 0)
            {
                Q.Factor = -1;
                P.Factor = 1;
            }
            else
            {
                Q.Factor = 0;
                P.Factor = 0;
            }





        }

        protected void RotaciónIzquierda(Nodo<T> nodo)
        {
            var RootNode = nodo.Padre;
            var P = nodo;
            var Q = P.Derecho;
            var B = Q.Izquierdo;

            if(RootNode != null)
            {
                if(RootNode.Derecho == P)
                {
                    RootNode.Derecho = Q;
                }
                else
                {
                    RootNode.Izquierdo = Q;
                }
            } else
            {
                root = Q;
                root.Padre = null;
            }

            P.Derecho = Q.Izquierdo;
            Q.Izquierdo = P;
            P.Padre = Q;

            if(B != null)
                B.Padre = P;
                Q.Padre = RootNode;

            if(Q.Factor == 0)
            {
                P.Factor = 1;
                Q.Factor = -1;
            }
            else
            {
                P.Factor = 0;
                Q.Factor = 0;
            }
            
        }
    }
}
