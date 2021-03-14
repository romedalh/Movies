1. Widoki korzystają z modeli z projektu Movies.Logic (np. z klasy MovieDetails) Rozważyłbym wprowadzenie osobnych modeli, niezależnych od projektu Movies.Logic.
2. Dodałbym zabezpieczenia na wypadek gdyby film nie istniał (np. gdyby ktoś wywołał GetMovieDetailsQuery dla nieistniejącego filmu) i na wypadek gdyby api nie było dostępne.
3. Projekt praktycznie nie ma error handlingu. Poświęciłbym czas na zaprojektowanie stron błędu, mechanizmów przekazywania kodów błędów.
4. Zamiast pobierać dane w HomeController zaimplementowałbym API Http, które byłoby odpytywane przez stronę kliencką (JS).
5. Popracowałbym na architekturą rozwiązania - na szybko wstawiłem CQSa, jednak sam podział na foldery mógłby ulec zmianie.
6. Detale filmu ograniczyłem jedynie do podstawowych danych - nie pobieram osób, planet itp. Gdyby miał to zrobić to zastanowiłbym się nad osobnymi metodami API do pobrania tych danych
7. Dla uproszczenia parsuję url filmu, by wyciągnąć jego id. Później korzystam z tego id do odpytania api o detale filmu. Docelowo, zastanowiłbym się nad wykorzystaniem całego urla do pobrania detali. 
8. Dopisałbym więcej testów jednostkowych (dla każdego command/query handlera) + testy integracyjne dla repozytoriów.
9. Nie pracuję na codzień z EF Core, więc zdaję sobie sprawę, że migrowanie baz danych może być wykonywane trochę inaczej niż ja to zrobiłem (w Startup.cs). Jednak na potrzeby szybkiego stworzenia tego rozwiązania postanowiłem pójść na takie uproszczenia.
10. ConnectionString bazy danych jak i adres API zapisałbym w appsettings.json