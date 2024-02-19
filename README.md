# Fitness AI

Aplikacja do zarządzania treningami i wejściówkami na siłownię.

## Generowanie linków

- **Metoda Html.ActionLink**: Jest to metoda helpera HTML, która generuje link do akcji kontrolera. Jest to najprostsza metoda tworzenia linków, ale również najmniej elastyczna. 

Przykładowe użycie:
```csharp
@Html.ActionLink("Go to the Dashboard", "Dashboard", "Client")
```

- **Metoda Url.Action z tagiem `<a>`**: Metoda ta generuje URL do akcji i kontrolera, który następny jest używany jako wartość atrybutu href w tagu `<a>`. Ta metoda jest bardziej elastyczna, ponieważ pozwala wygenerować URL niezależnie od kontekstu. 

Przykładowe użycie:
```csharp
<a href="@Url.Action("Dashboard", "Client")">Go to the Dashboard</a> 
```

- **Tag helpery**: Tag helpery to najnowsza i najbardziej zaawansowana metoda tworzenia linków. Pozwalają na użycie atrakcyjnej składni do renderowania HTML-a. Możemy użyć atrybutów asp-controller i asp-action wewnątrz tagu `<a>` do wygenerowania URL-a.

Przykładowe użycie:
```csharp
<a asp-controller="Ticket" asp-action="Tickets" class="nav-link align-middle px-0 text-white"> <i class="fas fa-solid fa-calendar-check"></i> <span class="d-none d-sm-inline">Karnety</span> </a> 
```

## Obsługa bazy danych

-  Migracja bazy za pomocą terminala w projekcie `UI`:
```shell
dotnet ef migrations add InitialDatabase -p FitnessAI.Infrastructure -o Persistence/Migrations -s FitnessAI.WebUI

dotnet ef database update -p FitnessAI.Infrastructure -s FitnessAI.WebUI

dotnet ef database drop --force -p FitnessAI.Infrastructure -s FitnessAI.WebUI
```









## Do zrobienia

- Zmienić treść pierwszej strony, tylko grafiki i kolor oraz logo
- Dodać ikony w górnym menu
- Ustawienie kolorów zmienia stopkę i menu
- Grafiki do karuzeli zmienić
- Panel administracyjny górne menu wyjustowane do prawej strony.

- Zmienić panel boczny.
- Dodać zakładkę trenera AI i kontroler i widok.
- Przetrenować Ajax, który ładuje odpowiedzi, można to oszukać robiąc serwis, który za pomocą AJAX odbiera rzeczy z API.

### Karnety

- Przy próbie zakupu wyrzuca błąd 
```shell
System.ArgumentNullException: Value cannot be null. (Parameter 'cipherText')
   at FitnessAI.Infrastructure.Encryption.EncryptionService.DecryptStringFromBytesAes(Byte[] cipherText, Byte[] key, Byte[] iv) in D:\_code\dyplom\FitnessAI\FitnessAI.Infrastructure\Encryption\EncryptionService.cs:line 63
```
- W jaki sposób korzystamy z WebAPI i z okien modalnych?












## Do dodania

- Zrobić ładniejsze tabele do dodawania użytkowników.
- Zrobić koszy, że można kupić np. koszulki, ręczniki, suplementy.
- Brakuje działań w Ajax, ale może są w karnetach.
- przejrzeć całą aplikację i przypomnieć sobie wszystkie funkcjonalności
- pozmieniać nazwy grafik
- Logowanie, nie ma jak zresetować hasła.
- Logowanie długi proces wylogowywania

### Layout

Formularz logowania jest nieładny.

### Workout

- Specjalny widok, gdzie podczas ćwiczeń łatwo podejrzeć sobie informacje o danym ćwiczeniu i dodać obciążenia i powtórzenia. 

### Atlas ćwiczeń

- Zbiór wszystkich ćwiczeń, które do tej pory zostały dodane do aplikacji.
- Każde ćwiczenie, które użytkownik dodał indywidualnie do swojego planu treningowego, będzie widoczne w atlasie ćwiczeń z liczbą, ile osób je wykonuje.

### Plan treningowy

- Są to wybrane z Atlasu Ćwiczeń ćwiczenia, które są aktywne podczas workout.
- Jest historia planów treningowych i można je przeglądać.

### Trener AI

- Można dodawać ćwiczenia i robić opisy do atlasu ćwiczeń.
- Trener po zadaniu pytania opowiada jakie ćwiczenie proponuje i gdy poprosimy go o wygenerowanie ćwiczenia to podaje je w formacie:
    - Nazwa ćwiczenia
    - Opis ćwiczenia
    - Wskazówki i na co uważać
- Przed dodaniem ćwiczenia do Atlasu Ćwiczeń można edytować opis, dodać filmik na Youtube i wygenerować grafikę za pomocą AI.

### Dodawanie elementów strony głównej z poziomu panelu administracyjnego.

- Każdy moduł strony głównej powinien mieć możliwość włączania i wyłączania z poziomu panelu administracyjnego.
- Strona kontakt powinna zawierać dane kontaktowe, które można edytować z poziomu panelu administracyjnego.
- Stopka powinna mieć możliwość edycji z poziomu panelu administracyjnego
- Polityka prywatności powinna być edytowalna z poziomu panelu administracyjnego.

### Publikacja aplikacji na serwerze

- najlepiej na moim serwerze.

### Większa stopka

- zrobić stopkę z linkami, które można edytować.
- Np. trzy kolumny, jako kategorie i dzięki temu można dodawać link i potem pobierać listę.
- https://getbootstrap.com/docs/5.3/examples/footers/
### Contact

- Dodać wyświetlanie danych firmy i mapki, które można edytować w panelu administracyjnym.









## Bugs

- podczas rejestracji gdy użytkownik istnieje, nie ma komunikatu o tym, że użytkownik istnieje i system przechodzi na nowo do strony rejestracji.
- Okno po potwierdzeniu rejestracji, nie ma linku do logowania, ani strony głównej.
- Wszystkie tabele mają angielskie nazwy

### Moje dane

Nie mam możliwości dodania swojego zdjęcia jako pracownik/trener.

### Client

- Data rejestracji jest zawsze Monday, January 1, 0001r.
- Trzeba dopisać, że nr domu i mieszkania w opisie pola
- Gdy wcisnę Konto firmowe, to przechodzi nawet bez NIP.
- Czy Klienci są przypisani do trenera
- Gdy jest lista klientów administrator powinien widzieć klienta i przypisanego mu trenera. Klient może być przypisany do kilku trenerów.
- Gdy usunę klienta to nie odświeża się na nowo strona, a dopiero wtedy ładuje się na nowo informacja  i działa filtr usuniętych.
- Klient nieaktywny powinien być wyszarzany w tabeli.
- Nie ma możliwości przywrócenia klienta w edycji.

### Pracownik

- Przy dodawaniu pracownika data zatrudnienia zaczyna się od 1 stycznia 0001r, trzeba ustwić dziś, tak jak w dacie zwolnienia.
- Możliwość dodawania stanowisk, bo jest już możliwość dodawania ról, ale po co i jak działa utoryzacja, gdy zmienię nazwę roli.
- Przy próbie dodania zdjęcia przenosi do dodawania plików i kasuje już wprowadzone dane.
- Strona trenerer wyświetla się na /trener/trener
- Strona osobista powinna możliwa być do dodania tylko dla trenera, a nie innego stanowiska.
- Wyświetlanie wszystkich trenerów.
- Gdzie jest wyświetlane zdjęcie trenera?
- Na stronie trenera od razu powinno być widoczne jego imię, nazwisko, zdjęcie i krótkie bio, a pod spodem dopiero informacje które poda. 

### Pliki

- Po wysłaniu plików, nie odświeża się strona.

### Lokalizacja

- Nie widzę działania

### Pulpit

- Administrator może dodawać Informacje wyświetlane na pulpicie.
- Wyświetla się teraz Witaj i mail, a powinna imię i nazwisko i nazwa użytkownika pod spodem.
- Wyświetlanie na pulpicie realnej ilości treningów pobieranej z bazy.
- Mogę oznaczyć ogłoszenie jako przeczytane, ale mam przełącznik do wyświetlania tych już przeczytanych.

### Kalendarz treningów

- W kalendarzu mogę wybrać rodzaj treningu i dzięki temu wyświetlają mi się te treningi na pulpicie.











## Cleanup Code

- Uruchomić w projektach nullable i wyczyścić wszystkie błędy. UWAGA!!! nie można się zalogować.
- Pozmieniać nazwy serwisów, które mają na początku I