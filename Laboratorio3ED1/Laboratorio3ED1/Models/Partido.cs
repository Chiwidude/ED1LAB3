using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratorio3ED1.Models
{
    public class Partido : IComparable
    {

        private DateTime fecha;
        private string Equipo1, Equipo2, grupo, estadio;
        private int Npartido;
        

        /// <summary>
        /// Constructor Clase Partido 
        /// </summary>
        /// <param name="fecha"></param>Fecha en la que se realizará el partido
        /// <param name="equipo1"></param> Equipo 1
        /// <param name="equipo2"></param>Equipo 2
        /// <param name="grupo"></param> grupo al que pertenecen
        /// <param name="estadio"></param>Estadio donde se realizará el encuentro
        /// <param name="npartido"></param>Número de partido 
        public Partido(DateTime fecha, string equipo1, string equipo2, string grupo, string estadio, int npartido)
        {
            this.Fecha = fecha;
            Equipo1_ = equipo1;
            Equipo2_ = equipo2;
            this.Grupo = grupo;
            this.Estadio = estadio;
            Npartido_ = npartido;
            
        }
        public Partido()
        {
            this.Fecha = new DateTime();
            this.Equipo1_ = null;
            this.Equipo2_ = null;
            this.Estadio = null;
            this.Grupo = null;
            this.Npartido_ = default(int);
        }

        [Display(Name = "Fecha de partido")]
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }

        [Display(Name = "País 1")]
        public string Equipo1_ { get { return Equipo1; } set { Equipo1 = value; } }

        [Display(Name = "País 2")]
        public string Equipo2_ { get { return Equipo2; } set { Equipo2 = value; } }

        [Display(Name = "Grupo")]
        public string Grupo { get { return grupo; } set { grupo = value; } }

        [Display(Name = "Estadio")]
        public string Estadio { get { return estadio; } set { estadio = value; } }

        [Display(Name = "No. de Partido")]
        public int Npartido_ { get { return Npartido; } set { Npartido = value; } }

        /*
        [Display(Name = "Fecha de partido")]
        public DateTime Fecha { get => fecha; set => fecha = value; }
        [Display(Name = "País 1")]
        public string Equipo1_ { get => Equipo1; set => Equipo1 = value; }
        [Display(Name = "País 2")]
        public string Equipo2_ { get => Equipo2; set => Equipo2 = value; }
        [Display(Name = "Grupo")]
        public string Grupo { get => grupo; set => grupo = value; }
        [Display(Name = "Estadio")]
        public string Estadio { get => estadio; set => estadio = value; }
        [Display(Name = "No. de Partido")]
        public int Npartido_ { get => Npartido; set => Npartido = value; }
        */
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

 }
