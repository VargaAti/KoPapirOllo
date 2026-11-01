const int GyozelmiPont = 3;

int nyertMerkozesek = 0;
int vesztettMerkozesek = 0;
int nyertKorok = 0;
int vesztettKorok = 0;
int dontetlenKorok = 0;
char menupont;

do
{
    Console.Clear();
    Console.WriteLine("=== KŐ – PAPÍR – OLLÓ ===");
    Console.WriteLine("[1] Új mérkőzés");
    Console.WriteLine("[2] Statisztika");
    Console.WriteLine("[3] Statisztika nullázása");
    Console.WriteLine("[K] Kilépés");
    Console.Write("Választás: ");

    menupont = Console.ReadKey().KeyChar;
    Console.WriteLine();

    switch (menupont)
    {
        case '1':
            bool ujraJatszik;

            do
            {
                int jatekosPont = 0;
                int gepPont = 0;
                bool megszakitva = false;

                Console.Clear();
                Console.WriteLine($"Új mérkőzés! Aki előbb eléri a {GyozelmiPont} pontot, nyer.");

                while (jatekosPont < GyozelmiPont && gepPont < GyozelmiPont && !megszakitva)
                {
                    Console.Write("Választásod (k = kő, p = papír, o = olló, m = főmenü): ");
                    string beirtValasztas = Console.ReadLine();

                    string jatekosValasztas = beirtValasztas switch
                    {
                        "k" or "K" => "kő",
                        "p" or "P" => "papír",
                        "o" or "O" => "olló",
                        "m" or "M" => "főmenü",
                        _ => "hibás"
                    };

                    if (jatekosValasztas == "főmenü")
                    {
                        megszakitva = true;
                        break;
                    }

                    if (jatekosValasztas == "hibás")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Csak k, p, o vagy m betűt adhatsz meg!");
                        Console.ResetColor();
                        continue;
                    }

                    int gepSzama = Random.Shared.Next(1, 4);
                    string gepValasztasa = gepSzama switch
                    {
                        1 => "kő",
                        2 => "papír",
                        _ => "olló"
                    };

                    if (jatekosValasztas == gepValasztasa)
                    {
                        dontetlenKorok++;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Te: {jatekosValasztas}, gép: {gepValasztasa} — döntetlen.");
                        Console.ResetColor();
                    }
                    else if ((jatekosValasztas == "kő" && gepValasztasa == "olló") ||
                             (jatekosValasztas == "olló" && gepValasztasa == "papír") ||
                             (jatekosValasztas == "papír" && gepValasztasa == "kő"))
                    {
                        jatekosPont++;
                        nyertKorok++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Te: {jatekosValasztas}, gép: {gepValasztasa} — ezt a kört te nyerted!");
                        Console.ResetColor();
                    }
                    else
                    {
                        gepPont++;
                        vesztettKorok++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Te: {jatekosValasztas}, gép: {gepValasztasa} — ezt a kört a gép nyerte.");
                        Console.ResetColor();
                    }

                    Console.WriteLine($"Állás: te {jatekosPont} – {gepPont} gép");
                }

                ujraJatszik = false;

                if (!megszakitva)
                {
                    if (jatekosPont == GyozelmiPont)
                    {
                        nyertMerkozesek++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Megnyerted a mérkőzést {jatekosPont}:{gepPont} arányban!");
                        Console.ResetColor();
                    }
                    else
                    {
                        vesztettMerkozesek++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"A gép nyerte a mérkőzést {gepPont}:{jatekosPont} arányban.");
                        Console.ResetColor();
                    }

                    char ujrajatszas;
                    bool helyesValasz;

                    do
                    {
                        Console.Write("Akarsz újra játszani? (i/n): ");
                        ujrajatszas = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        helyesValasz = ujrajatszas == 'i' || ujrajatszas == 'I' ||
                                       ujrajatszas == 'n' || ujrajatszas == 'N';

                        if (!helyesValasz)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Csak i vagy n betűt adhatsz meg!");
                            Console.ResetColor();
                        }
                    }
                    while (!helyesValasz);

                    ujraJatszik = ujrajatszas == 'i' || ujrajatszas == 'I';
                }
            }
            while (ujraJatszik);
            break;

        case '2':
            Console.Clear();
            Console.WriteLine("--- Statisztika ---");

            int osszesMerkozes = nyertMerkozesek + vesztettMerkozesek;
            int osszesKor = nyertKorok + vesztettKorok + dontetlenKorok;

            Console.WriteLine($"Mérkőzések: {osszesMerkozes} (nyert: {nyertMerkozesek}, vesztett: {vesztettMerkozesek})");

            if (osszesMerkozes > 0)
            {
                double gyozelmiArany = (double)nyertMerkozesek / osszesMerkozes;
                Console.WriteLine($"Győzelmi arány: {gyozelmiArany:P0}");
            }

            Console.WriteLine($"Körök: {osszesKor} (nyert: {nyertKorok}, vesztett: {vesztettKorok}, döntetlen: {dontetlenKorok})");
            Console.WriteLine();
            Console.Write("A főmenühöz nyomj meg egy billentyűt...");
            Console.ReadKey();
            break;

        case '3':
            Console.Clear();
            char nullazas;
            bool ervenyesValasz;

            do
            {
                Console.Write("Biztosan nullázod a statisztikát? (i/n): ");
                nullazas = Console.ReadKey().KeyChar;
                Console.WriteLine();

                ervenyesValasz = nullazas == 'i' || nullazas == 'I' ||
                                  nullazas == 'n' || nullazas == 'N';

                if (!ervenyesValasz)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Csak i vagy n betűt adhatsz meg!");
                    Console.ResetColor();
                }
            }
            while (!ervenyesValasz);

            if (nullazas == 'i' || nullazas == 'I')
            {
                nyertMerkozesek = 0;
                vesztettMerkozesek = 0;
                nyertKorok = 0;
                vesztettKorok = 0;
                dontetlenKorok = 0;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A statisztikát nulláztad.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("A nullázást megszakítottad.");
            }

            Console.Write("A főmenühöz nyomj meg egy billentyűt...");
            Console.ReadKey();
            break;

        case 'k':
        case 'K':
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nincs ilyen menüpont!");
            Console.ResetColor();
            Console.Write("A folytatáshoz nyomj meg egy billentyűt...");
            Console.ReadKey();
            break;
    }
}
while (menupont != 'k' && menupont != 'K');
