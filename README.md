## Instrukcja uruchomienia
1. Otwórz pobrane repozytorium
2. Uruchom projekt
3. Konsola automatycznie wykona i wyświetli gotowy scenariusz demonstracyjny.

## Kohezja, Coupling i Odpowiedzialność

**1. Podział klas i warstw:**
Podzieliłem projekt na dwa foldery: `Models` (dane) i `Services` (logika). Modele to zwykłe pojemniki na informacje, a całą prace z zarządzaniem systemem zrzuciłem do klasy `RentalService`.

**2. Odpowiedzialność (Kohezja):**
Dobrze to widać na klasie `RentalService`. Modele takie jak `User` czy `Equipment` nie mają w sobie logiki biznesowej – same nie potrafią się wypożyczać ani liczyć kar. Całą prace z limitami i opłatami wykonuje serwis.

**3. Coupling (Sprzężenie):**
Żeby nie robić bałaganu i nie wiązać na sztywno Użytkownika ze Sprzętem, dorzuciłem klasę `Rental`. Zamiast wpychać listę wypożyczonych rzeczy bezpośrednio do obiektu studenta, ta klasa spina to kto co i na jak długo wziął.