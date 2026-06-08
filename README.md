# APBD_Cwiczenie8_CodeFirst-
aplikacja ASP.NET Core MVC do obsługi książek w bibliotece kursowej. Projekt używa Entity Framework Core w podejściu Code First.

## Jak uruchomić aplikację

1. Sklonuj albo pobierz projekt.
2. Otwórz terminal w folderze projektu.
3. Przywróć paczki:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

## Jak utworzyć albo odtworzyć bazę
używając komendy bash
```bash
dotnet ef database update
```
Connection string znajduje się w pliku appsettings.json.

## Jaką komendą utworzono migrację
Do stworzenia migracji komendy użyto

```bash
dotnet ef migrations add InitialCreate
```

## Gdzie jest DbContext
DbContext znajduje się w pliku: Data/LibraryDbContext.cs

W klasie: LibraryDbContext

## Gdzie jest konfiguracja relacji
Konfiguracja relacji znajduje się w metodzie OnModelCreating w pliku: Data/LibraryDbContext.cs

## Gdzie jest seeding
Seeding znajduje się w pliku: Data/LibrarySeeder.cs

Seeder jest wywoływany w Program.cs.

# Pytania

## Co oznacza ORM i jaki problem rozwiązuje EF Core?
ORM, czyli Object-Relational Mapping, to mechanizm mapowania obiektów C# na tabele w bazie danych. EF Core rozwiązuje problem ręcznego pisania dużej ilości kodu SQL dla typowych operacji na danych. Dzięki temu można pracować na klasach C#, a EF Core tłumaczy operacje na zapytania SQL.

## Jaka jest rola DbContext?
DbContext reprezentuje połączenie aplikacji z bazą danych. Przechowuje konfigurację modelu, udostępnia DbSet dla encji i śledzi zmiany w obiektach. Dzięki temu EF Core wie, jakie dane dodać, zmienić albo usunąć podczas wywołania SaveChanges.

## Czym DbSet różni się od zwykłej listy w C#?
DbSet reprezentuje tabelę w bazie danych i pozwala wykonywać zapytania, dodawać, usuwać oraz aktualizować rekordy. Zwykła lista działa tylko w pamięci programu. DbSet może tłumaczyć operacje LINQ na SQL i pobierać dane z bazy.

## Dlaczego DbContext w aplikacji webowej powinien być Scoped?
DbContext powinien być Scoped, ponieważ jedna instancja jest wtedy używana w ramach jednego żądania HTTP. To pozwala bezpiecznie śledzić zmiany w danym żądaniu i unikać problemów z równoczesnym używaniem tego samego kontekstu przez wielu użytkowników.

## Co robi migracja EF Core?
Migracja opisuje zmiany w strukturze bazy danych wynikające ze zmian w modelach C#. Może tworzyć tabele, kolumny, klucze obce, indeksy i inne elementy bazy. Dzięki migracjom można kontrolować historię zmian schematu bazy danych.

## Dlaczego seeding powinien być idempotentny?
Seeding powinien być idempotentny, ponieważ ponowne uruchomienie aplikacji nie powinno dublować tych samych danych. Jeśli seeder dodawałby dane za każdym razem, w bazie szybko pojawiłyby się powtórzone rekordy.

## Kiedy Code First jest dobrym wyborem, a kiedy lepiej rozważyć Database First?
Code First jest dobrym wyborem, gdy aplikacja i baza są tworzone od początku, a model danych ma wynikać z klas C#. Database First warto rozważyć, gdy baza danych już istnieje, ma ustaloną strukturę albo jest zarządzana niezależnie od aplikacji.