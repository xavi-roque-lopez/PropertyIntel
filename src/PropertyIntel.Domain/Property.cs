using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyIntel.Domain
{
    public class Property
    {
        // Usamos 'init' o 'private set' para proteger el estado de la entidad
        public Guid Id { get; init; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } // ¡Siempre decimal para dinero!
        public double Surface { get; private set; }
        public int Rooms { get; private set; }
        public int Bathrooms { get; private set; }
        public int? Floor { get; private set; } // Nullable, porque una casa unifamiliar no tiene piso
        public bool HasElevator { get; private set; }
        public bool HasTerrace { get; private set; }
        public bool HasParking { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string City { get; private set; } = string.Empty;
        public string Neighbourhood { get; private set; } = string.Empty;
        public string Agency { get; private set; } = string.Empty;
        public string Source { get; private set; } = string.Empty; // Ej: Portal_A, Inmobiliaria_B
        public string Url { get; private set; } = string.Empty;
        public string Images { get; private set; } = string.Empty;
        public DateTime PublishedDate { get; private set; }
        public DateTime LastSeen { get; private set; }

        // Constructor vacío privado requerido para EF Core y deserialización
        private Property()
        {
        }

        // Constructor público que obliga a que toda propiedad nazca con datos válidos
        public Property(
            Guid id, string title, string description, decimal price, double surface,
            int rooms, int bathrooms, int? floor, bool hasElevator, bool hasTerrace,
            bool hasParking, double latitude, double longitude, string city,
            string neighbourhood, string agency, string source, string url,
            DateTime publishedDate)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("El título es obligatorio.") : title;
            Description = description;
            Price = price < 0 ? throw new ArgumentException("El precio no puede ser negativo.") : price;
            Surface = surface;
            Rooms = rooms;
            Bathrooms = bathrooms;
            Floor = floor;
            HasElevator = hasElevator;
            HasTerrace = hasTerrace;
            HasParking = hasParking;
            Latitude = latitude;
            Longitude = longitude;
            City = city;
            Neighbourhood = neighbourhood;
            Agency = agency;
            Source = source;
            Url = url;
            PublishedDate = publishedDate;
            LastSeen = DateTime.UtcNow;
        }

        // Un método de dominio para actualizar el precio con lógica de negocio y registrar auditoría
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("El nuevo precio debe ser mayor a cero.");
            Price = newPrice;
            LastSeen = DateTime.UtcNow;
        }
    }
}
