namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Deportivo: ICoche
    {

        public Deportivo()
        {
            this.Marca = "BMW";
            this.Modelo = "E46";
            this.Imagen = "e46.webp";
            this.Velocidad = 0;
            this.VelocidadMaxima = 270;
        }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }

        public void Acelerar()
        {
            this.Velocidad += 50;
            if(this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }
        public void Frenar()
        {
            this.Velocidad -= 30;
            if (this.Velocidad <= 0)
            {
                this.Velocidad = 0;
            }
        }


    }
}
