using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensionsExercise
{
    public static void SeedExercise(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>().HasData(
            new Exercise
            {
                Id = 1,
                Title = "Pompki",
                Category = "Kalistenika",
                Description =
                    "Pompki to jedno z najbardziej popularnych ćwiczeń na mięśnie klatki piersiowej, tricepsów i przedramion.",
                WorkoutInstruction =
                    "1. Ułóż się na podłodze, opierając się na dłoniach i palcach stóp. 2. Wykonaj pompkę, opuszczając ciało do momentu, gdy klatka dotknie podłogi. 3. Wróć do pozycji wyjściowej.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x15",
                AdvancedLoad = "3x20",
                ImageUrl = "/images/exercise/pompki.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=baCcDGahK-g",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 2,
                Title = "Przysiady",
                Category = "Kalistenika",
                Description = "Przysiady to podstawowe ćwiczenie angażujące głównie mięśnie nóg i pośladków.",
                WorkoutInstruction =
                    "1. Stań prosto z nogami na szerokość barków. 2. Zginaj kolana, opuszczając ciało jak gdybyś chciał usiąść na niewidzialnym krześle. 3. Wróć do pozycji wyjściowej.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x15",
                AdvancedLoad = "3x20",
                ImageUrl = "/images/exercise/przysiady.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=gC2JDbEuwOI",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 3,
                Title = "Martwy ciąg",
                Category = "Ciężary",
                Description =
                    "Martwy ciąg to ćwiczenie siłowe, które angażuje wiele grup mięśniowych, w tym mięśnie pleców, nogi i pośladki.",
                WorkoutInstruction =
                    "1. Stań przy sztandze tak, by gryf znajdował się nad środkiem stóp. 2. Schyl się i chwyć sztangę. 3. Podnieś sztangę do góry, prostując nogi i plecy. 4. Opuszczaj sztangę kontrolowanym ruchem.",
                BeginnerLoad = "3x8",
                IntermediateLoad = "3x10",
                AdvancedLoad = "3x12",
                ImageUrl = "/images/exercise/martwy_ciag.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=0_igODjLiXM",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 4,
                Title = "Bieganie",
                Category = "Kardio",
                Description =
                    "Bieganie to świetny sposób na poprawę kondycji sercowo-naczyniowej, spalanie kalorii i poprawę nastroju.",
                WorkoutInstruction =
                    "1. Rozpocznij od rozgrzewki. 2. Biegaj w umiarkowanym tempie przez wybrany czas lub dystans. 3. Zakończ ćwiczenie chłodzeniem.",
                BeginnerLoad = "30 min",
                IntermediateLoad = "45 min",
                AdvancedLoad = "60 min",
                ImageUrl = "/images/exercise/bieganie.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=PoTl0NK1Qvw",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 5,
                Title = "Skakanka",
                Category = "Kardio",
                Description =
                    "Skakanie na skakance to efektywny sposób na poprawę koordynacji, spalanie kalorii i wzmacnianie mięśni nóg.",
                WorkoutInstruction =
                    "1. Stań prosto trzymając skakankę. 2. Skacz, obracając skakankę pod stopami. 3. Utrzymuj równomierny rytm.",
                BeginnerLoad = "10 min",
                IntermediateLoad = "15 min",
                AdvancedLoad = "20 min",
                ImageUrl = "/images/exercise/skakanka.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=Depy5QLMAyg",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 6,
                Title = "Wyciskanie sztangi",
                Category = "Ciężary",
                Description =
                    "Wyciskanie sztangi to podstawowe ćwiczenie na klatkę piersiową, triceps i przednie części barków.",
                WorkoutInstruction =
                    "1. Połóż się na ławce poziomej. 2. Chwyć sztangę na szerokość barków. 3. Opuszczaj sztangę do klatki piersiowej. 4. Wyciskaj do pełnego wyprostu ramion.",
                BeginnerLoad = "3x8",
                IntermediateLoad = "3x10",
                AdvancedLoad = "3x12",
                ImageUrl = "/images/exercise/wyciskanie_sztangi.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=8_33og5lN-Y",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 7,
                Title = "Podciąganie na drążku",
                Category = "Kalistenika",
                Description =
                    "Podciąganie na drążku to efektywne ćwiczenie na mięśnie grzbietu, biceps oraz przedramiona.",
                WorkoutInstruction =
                    "1. Chwyć drążek nachwytem na szerokość barków. 2. Podciągnij ciało, aż broda znajdzie się nad drążkiem. 3. Opuszczaj ciało kontrolowanie do pozycji wyjściowej.",
                BeginnerLoad = "3x6",
                IntermediateLoad = "3x8",
                AdvancedLoad = "3x10",
                ImageUrl = "/images/exercise/podciaganie_na_drazku.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=jxQ6lKiVfuo",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 8,
                Title = "Wiosłowanie sztangą",
                Category = "Ciężary",
                Description = "Wiosłowanie sztangą to ćwiczenie angażujące mięśnie pleców, biceps oraz ramiona.",
                WorkoutInstruction =
                    "1. Stań w lekkim rozkroku, trzymając sztangę. 2. Schyl się w pasie, trzymając plecy prosto. 3. Ciągnij sztangę do dolnej części brzucha. 4. Opuszczaj sztangę kontrolowanie.",
                BeginnerLoad = "3x8",
                IntermediateLoad = "3x10",
                AdvancedLoad = "3x12",
                ImageUrl = "/images/exercise/wioslowanie_sztanga.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=MOlDmANU_4U",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            }
            ,
            new Exercise
            {
                Id = 9,
                Title = "Wyciskanie hantli",
                Category = "Ciężary",
                Description =
                    "Wyciskanie hantli to ćwiczenie na klatkę piersiową, które pozwala na pracę nad symetrią i równowagą siły.",
                WorkoutInstruction =
                    "1. Połóż się na ławce poziomej z hantlą w każdej ręce. 2. Wyciskaj hantle do góry, aż ręce będą w pełnym wyproście. 3. Opuszczaj hantle do poziomu klatki piersiowej.",
                BeginnerLoad = "3x8",
                IntermediateLoad = "3x10",
                AdvancedLoad = "3x12",
                ImageUrl = "/images/exercise/wyciskanie_hantli.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=ACsYCmLA9Do",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 10,
                Title = "Prostowanie ramion w siadzie",
                Category = "Ciężary",
                Description =
                    "Prostowanie ramion w siadzie to ćwiczenie izolujące triceps, wykonane z hantlą lub sztangielką.",
                WorkoutInstruction =
                    "1. Siądź na ławce z hantlą trzymaną oburącz za głową. 2. Prostuj ramiona, unosząc ciężar nad głowę. 3. Powoli opuszczaj ciężar za głowę.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/prostowanie_ramion_w_siadzie.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=mJf7Q8_nJMk",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 11,
                Title = "Uginanie ramion ze sztangą",
                Category = "Ciężary",
                Description = "Uginanie ramion ze sztangą to ćwiczenie na biceps, wykonane ze sztangą.",
                WorkoutInstruction =
                    "1. Stań prosto, trzymając sztangę chwytem podchwytem. 2. Uginaj ramiona, podnosząc sztangę do barków. 3. Powoli opuszczaj sztangę do pozycji wyjściowej.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/uginanie_ramion_ze_sztanga.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=wHbgdQ5rS7g",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 12,
                Title = "Uginanie ramion ze sztangielkami",
                Category = "Ciężary",
                Description = "Uginanie ramion ze sztangielkami pozwala na pracę nad symetrią i siłą bicepsów.",
                WorkoutInstruction =
                    "1. Stań prosto, trzymając sztangielkę w każdej ręce. 2. Uginaj ramiona na przemian, podnosząc sztangielki do barków. 3. Powoli opuszczaj sztangielki.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/uginanie_ramion_ze_sztangielkami.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=0gHGJwOJqVw",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 13,
                Title = "Wyciskanie na maszynie",
                Category = "Ciężary",
                Description =
                    "Wyciskanie na maszynie to ćwiczenie na klatkę piersiową, które zapewnia stabilizację ruchu.",
                WorkoutInstruction =
                    "1. Usiądź na maszynie do wyciskania. 2. Chwyć uchwyty na wysokości klatki piersiowej. 3. Wyciskaj ręce do przodu. 4. Powoli wracaj do pozycji wyjściowej.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/wyciskanie_na_maszynie.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=DgasTNLyQcg",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 14,
                Title = "Prostowanie nóg na maszynie",
                Category = "Ciężary",
                Description = "Prostowanie nóg na maszynie to ćwiczenie izolujące mięśnie czworogłowe uda.",
                WorkoutInstruction =
                    "1. Usiądź na maszynie do prostowania nóg. 2. Umieść podudzia pod wałek. 3. Prostuj nogi, unosząc wałek. 4. Powoli opuszczaj wałek.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/prostowanie_nog_na_maszynie.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=aTs1YmgK5m8",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            },
            new Exercise
            {
                Id = 15,
                Title = "Uginanie nóg na maszynie",
                Category = "Ciężary",
                Description = "Uginanie nóg na maszynie to ćwiczenie na mięśnie dwugłowe uda.",
                WorkoutInstruction =
                    "1. Połóż się na brzuchu na maszynie do uginania nóg. 2. Uginaj nogi w kolanach, przyciągając wałek do pośladków. 3. Powoli opuszczaj wałek.",
                BeginnerLoad = "3x10",
                IntermediateLoad = "3x12",
                AdvancedLoad = "3x15",
                ImageUrl = "/images/exercise/uginanie_nog_na_maszynie.png",
                MultimediaUrl = "https://www.youtube.com/watch?v=ZAkgAX2lcZk",
                CreatedDate = new DateTime(2024, 1, 12),
                IsActive = true
            }
        );
    }
}