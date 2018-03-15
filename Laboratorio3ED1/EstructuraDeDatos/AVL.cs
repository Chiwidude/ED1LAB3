using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
    public class AVL<T> : IAVL<T> where T: IComparable
    {
        
        public Nodo<T> root;
        CompareTo<T> Comparador;

        

        public AVL()
        {
            root = null;
            
        }
        public AVL(CompareTo<T> compare)
        {
            root = null;
            
            this.Comparador = compare;
        }
        public Nodo<T> Buscar(T value)
        {
            var auxiliar = root;
            while (Comparador(auxiliar.value,value) != 0)
            {
                if (Comparador(value, auxiliar.value) < 0)
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
            var auxiliar = root;
            var padre = null as Nodo<T>;
            var esHijoIz = true;
            while (Comparador(auxiliar.value,value) != 0)
            {
                padre = auxiliar;
                if (Comparador(value, auxiliar.value) < 0)
                {
                    esHijoIz = true;
                    auxiliar = auxiliar.Izquierdo;
                }
                else
                {
                    esHijoIz = false;
                    auxiliar = auxiliar.Derecho;
                }
                if (auxiliar == null)
                {
                    return null;
                }
            }// Fin ciclo inicial

            if (auxiliar.Izquierdo == null && auxiliar.Derecho == null) // nodo hoja
            {
                if (padre != null)
                {

                    if (esHijoIz)
                    {
                        padre.Izquierdo = null;
                    }
                    else
                    {
                        padre.Derecho = null;
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    root = null;
                }
            }
            else if (auxiliar.Derecho == null) // solo hijo Izquierdo
            {
                if (padre != null)
                {
                    if (esHijoIz)
                    {
                        padre.Izquierdo = auxiliar.Izquierdo;
                        auxiliar.Izquierdo.Padre = padre;
                    }
                    else
                    {
                        padre.Derecho = auxiliar.Izquierdo;
                        auxiliar.Izquierdo.Padre = padre;
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    auxiliar.Izquierdo.Padre = null;
                    root = auxiliar.Izquierdo;
                }
            }
            else if (auxiliar.Izquierdo == null) //solo hijo Derecho
            {
                if (padre != null)
                {

                    if (esHijoIz)
                    {
                        padre.Izquierdo = auxiliar.Derecho;
                        auxiliar.Derecho.Padre = padre;
                    }
                    else
                    {
                        padre.Derecho = auxiliar.Derecho;
                        auxiliar.Derecho.Padre = padre;
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    auxiliar.Derecho.Padre = null;
                    root = auxiliar.Derecho;
                }
            }

            else // Tiene dos hijos
            {
                var reemplazo = Reemplazar(auxiliar);
                if (root == auxiliar)
                {
                    root = reemplazo;
                    root.Padre = null;
                    Balancear(root, false, esHijoIz);
                   
                }
                else if (esHijoIz)
                {
                    padre.Izquierdo = reemplazo;
                    padre.Izquierdo.Padre = padre;
                    Balancear(padre.Izquierdo, false, esHijoIz);
                }
                else
                {
                    padre.Derecho = reemplazo;
                   
                }
                reemplazo.Izquierdo = auxiliar.Izquierdo;
                
            }
                return auxiliar;
       }
        

        /// <summary>
        /// Elimina un Nodo mediante sustitucion
        /// </summary>
        /// <param name="Nodoelmiminar">Nodo a Eliminar </param>
        /// <returns>Nodo de Reemplazo</returns>
        private static Nodo<T> Reemplazar(Nodo<T> Nodoelmiminar)
        {
            var reemplazopadre = Nodoelmiminar;
            var reemplazo = Nodoelmiminar;
            var auxiliar = Nodoelmiminar.Derecho;
            while (auxiliar != null)
            {
                reemplazopadre = reemplazo;
                reemplazo = auxiliar;
                auxiliar = auxiliar.Izquierdo;
            }
            if (reemplazo != Nodoelmiminar.Derecho)
            {
                reemplazopadre.Izquierdo = reemplazo.Derecho;
                reemplazo.Derecho = Nodoelmiminar.Derecho;
            }
            return reemplazo;
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
                if (Comparador(newnode.value,padre.value) < 0)
                {
                    if (padre.Izquierdo == null)
                    {
                        
                        padre.Izquierdo = newnode;
                        padre.Izquierdo.Padre = padre;
                        Balancear(padre, true, true);
                    }
                    else
                    {
                        InsertarInterno(newnode, padre.Izquierdo);
                    }
                }
                else
                {
                    if (Comparador(newnode.value,padre.value) > 0)
                    {
                        if (padre.Derecho == null)
                        {
                            
                            padre.Derecho = newnode;
                            padre.Derecho.Padre = padre;
                            Balancear(padre, true, false);
                        }
                        else
                        {
                            InsertarInterno(newnode, padre.Derecho);
                        }
                    }
                }
            }

        }
        

        #region Recorridos
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

        #endregion
        #region Rotaciones
        protected void RotaciónDerecha(Nodo<T> nodo)
        {
            var Rootparent = nodo.Padre;
            var Q = nodo as Nodo<T>;
            var P = Q.Izquierdo as Nodo<T>;
            var C = P.Derecho as Nodo<T>;

            if (Rootparent != null)
            {
                if (Rootparent.Derecho == Q)
                {
                    Rootparent.Derecho = P;
                }
                else
                {
                    Rootparent.Izquierdo = P;
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

        protected void RotacionDDerecha(Nodo<T> nodo)
        {
            var RootParent = nodo.Padre;
            var P = nodo as Nodo<T>;
            var Q = P.Izquierdo;
            var R = Q.Derecho;
            var B = R.Izquierdo;
            var C = R.Derecho;

            if(RootParent != null)
            {
                if(RootParent.Derecho == P)
                {
                    RootParent.Derecho = R;
                }
                else
                {
                    RootParent.Izquierdo = R;
                }
            } else
            {
                root = R;
                root.Padre = null;
            }

            Q.Derecho = B;
            P.Izquierdo = C;
            R.Izquierdo = Q;
            R.Derecho = P;

            R.Padre = RootParent;
            P.Padre = Q.Padre = R;

            if (B != null)
                B.Padre = Q;
            if(C != null)
            {
                C.Padre = P;
            }

            switch (R.Factor)
            {
                case -1:
                    {
                        Q.Factor = 0;
                        P.Factor = 1;
                    }
                    break;
                case 0:
                    {
                        Q.Factor = 0;
                        P.Factor = 0;
                      
                    }break;
                case 1:
                    {
                        Q.Factor = -1;
                        P.Factor = 0;
                    }break;
            }
            R.Factor = 0;

        }

        protected void RotacionDIzquierda(Nodo<T> nodo)
        {
            var RParent = nodo.Padre;
            var P = nodo;
            var Q = nodo.Derecho;
            var R = Q.Izquierdo;
            var B = R.Izquierdo;
            var C = R.Derecho;

            if(RParent != null)
            {
                if(RParent.Derecho == P)
                {
                    RParent.Derecho = R;
                }
                else
                {
                    RParent.Izquierdo = R;
                }
            }
            P.Derecho = B;
            Q.Izquierdo = C;
            R.Izquierdo = P;
            R.Derecho = Q;

            R.Padre = RParent;
            P.Padre = Q.Padre = R;

            if(B != null)
            {
                B.Padre = P;
            }
            if (C != null)
                C.Padre = Q;
            switch (R.Factor)
            {
                case -1:
                    {
                        P.Factor= 0;
                        Q.Factor = 1;
                    }
                    break;

                case 0:
                    {
                        P.Factor = 0;
                        Q.Factor = 0;
                    }
                    break;

                case 1:
                    {
                        P.Factor = -1;
                        Q.Factor = 0;
                    }
                    break;
            }




        }
        private void Balancear(Nodo<T> node, bool isnew, bool isleft)
        {
            var exit = false;

            while ((node != null) && !exit)
            {
                var didRotate = false;

                if (isnew)
                {
                    if (isleft)
                    {
                        node.Factor--;
                    }
                    else
                    {
                        node.Factor++;
                    }
                }
                else
                {
                    if (node.Factor == 0)

                        exit = true;

                    if (isleft)

                        node.Factor++;

                    else

                        node.Factor--;


                }
                if (node.Factor == 0)
                    exit = true;

                else if (node.Factor == -2)
                {
                    if (node.Izquierdo.Factor == 1)
                    {
                        RotacionDDerecha(node);
                        didRotate = true;
                    }
                    else
                    {
                        RotaciónDerecha(node);
                        didRotate = true;
                    }
                    exit = true;
                }
                else if (node.Factor == 2)
                {
                    if (node.Derecho.Factor == -1)
                    {
                        RotacionDIzquierda(node);
                        didRotate = true;
                    }
                    else
                    {
                        RotaciónIzquierda(node);
                        didRotate = true;
                    }
                    exit = true;
                }
                if ((didRotate) && (node.Padre != null) && (!isnew))
                    node = node.Padre;

                if (node.Padre != null)
                {
                    if (node.Padre.Derecho == node)
                    {
                        isleft = false;
                    }
                    else
                    {
                        isleft = true;
                    }
                    if ((!isnew) && (node.Factor == 0))
                    {
                        exit = false;
                    }
                }
                node = node.Padre;

            }
        }
        #endregion

    }
}
