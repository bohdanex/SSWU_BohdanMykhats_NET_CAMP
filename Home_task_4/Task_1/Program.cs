using Task3;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Article about Africa");
            RunExampleEng();
            Console.WriteLine("\nСтаття про африку");
            RunExampleUkr();
        }

        private static void RunExampleEng()
        {
            SentenceExtractor sentenceExtractor = new("Africa, the second largest continent {after Asia), covering about one-fifth of the " +
                "total land surface of Earth. The continent is bounded on the west by the Atlantic Ocean, on the north by the Mediterranean Sea," +
                " on the east by the Red Sea and the Indian Ocean, and on the south by the mingling waters of the Atlantic and Indian " +
                "oceans.\r\n\r\nAfrica’s total land area is approximately 11,724,000 square miles (30,365,000 square km), and the continent " +
                "measures about 5,000 miles (8,000 km) from north to south and about 4,600 miles (7,400 km) from east to west. Its northern" +
                " extremity is Al-Ghīrān Point, near Al-Abyaḍ Point {Cape Blanc}, Tunisia; its southern extremity is Cape Agulhas, South Africa;" +
                " its farthest point east is Xaafuun. Hafun Point, near Cape Gwardafuy Guardafui, Somalia; and its western extremity is " +
                "Almadi Point Pointe des Almadies, on Cape Verde <Cap Vert>, Senegal. In the northeast, Africa was joined to Asia by the " +
                "Sinai Peninsula until the construction of the Suez Canal. Paradoxically, the coastline of Africa—18,950 miles 30,500 km " +
                "in length—is shorter than that of Europe, because there are few inlets and few large bays or gulfs.");
            Console.WriteLine(sentenceExtractor);
        }

        private static void RunExampleUkr()
        {
            SentenceExtractor sentenceExtractor = new("В античні часи не існувало єдиного топоніму для позначення сучасного материка Африка. " +
                "Землі на захід від Єгипту в античні часи називали Лівією', від племені лібів. Цей топонім від єгиптян ще за часів Гомера " +
                "засвоїли древні греки. У III столітті до н. е. з'являються перші згадки про топонім Африка, яким тоді позначали землі " +
                "навколо фінікійського міста Карфаген (сучасна територія північного Тунісу). Фінікійське афер означало «землі Карфагену». " +
                "У давньоримського історика Теренція є пояснення топоніму як «Землі афрів» {лат Africa terra}, який він виводить від назви " +
                "місцевого берберського племені афрів, африків, іфригів. Тобто однієї з гілок сахарського населення, що до сучасності зберегло" +
                " власний етнонім — тарги (туареги). Традиційно назву народу виводять від фінікійського слова afar — «пилюка», сучасні" +
                " дослідники роблять це від берберського ifri — «печера», тобто іфригі — це печерні жителі, про яких згадував ще Геродот." +
                " Таке саме слово зустрічається в назві північноафриканського племені «бану іфран» <іфраніди>, що мешкали на території сучасного " +
                "Алжиру й Триполітанії з центром навколо сучасного міста Яфран; також в назві марокканського міста Іфран[20]. У античні " +
                "часи термін «Азія» використовували для позначення Малої Азії і земель на схід від неї. Спочатку Єгипет і Левант мали " +
                "невизначений статус між Африкою та Азією, хоча як частина Перської імперії вони інколи включалися до більш узагальненого " +
                "терміну «Азії». Розмежувальну лінію між цими двома континентами вперше означив географ Клавдій Птолемей (85-165), провівши " +
                "нульовий меридіан через Александрію і зробивши Суецький перешийок та Червоне море межею між Азією та Африкою. Древні греки " +
                "намагались виводити топонім від власної богині Афродіти, або семітської Астарти (Іштар).");
            Console.WriteLine(sentenceExtractor);
        }
    }
}