using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    public enum PetBreedType 
    {
        Husky,
        Aussie,
        Dachshund,
        Chihuahua,
        Labrador,
        Pitbull,
        Yorkie,
        Samoyed,
        Brittany,
        Labordoodle,
        Poodle,
        Dalmatian,
        Goldendoodle,
        RodesianRidgeback,
        ShibaInu,
        WelshCorgi,
        GoldenRetriever,
        SaintBernard,
        
    }
    public enum PetColorType {
        Golden,
        Black,
        Grey,
        White,
        Brown,
        Dualcolor,
        Tricolor,
        Mixed,
        Spotted
    }



    public class Pet {
        public int id {get;set;}

        [Required]
        public string name {get;set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetBreedType breed {get;set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetColorType color {get;set;}
        
        public DateTime checkedIn {get;set;}

        public DateTime checkedOut {get;set;}

        [ForeignKey("bakedBy")]
        public int petOwnerId {get;set;}

        public PetOwner petOwner {get;set;}
    }
}
