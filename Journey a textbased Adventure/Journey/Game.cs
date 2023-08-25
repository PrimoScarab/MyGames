using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    /// <summary>
    /// Huvudklass för spelet.
    /// Initierar all data och hanterar spelloppen.
    /// </summary>
    class Game
    {
        enum Stage
        {
            Beginning,
            FirstThreshold,
            Ordeal,
            TheRoadBack,
            Ending
        };
        Stage stage;

        //Spelets tillgängliga ting

        private Random rand = new Random();

        public bool mainCharacterMove = true;
        public bool mentorCharacterMove = false;
        public bool sideKickMove = false;
        public bool looping = false;
        public bool mainCharactersAlive = true;
        public bool teamAlive = true;
        public bool orksAlive = true;
        public bool bossAlive = true;
        public bool running = true;
        public bool testBool = true;

        private List<Character> characters;
        private List<Location> locations;
        private List<Prop> props;
        private List<Weapon> weapons;
        

        //SPELETS KARAKTÄRER
        //Spelets huvudkaraktär
        private Character mainCharacter;
        private Character sideKick;
        private Character princess;
        private Character mentor;
        private Character villain;
        private Character ork1;
        private Character ork2;
        private Character ork3;

        //SPELETS PLATSER

        //Spelets starplats
        private Location startingLocation;

        //Spelets ordeal plats
        private Location firstThresHoldLocation;
        private Location ordealLocation1;
        private Location ordealLocation2;


        //SPELETS VAPEN
        //Spelets svärd
        private Weapon sword;
        private Weapon staf;
        

        public Game()
        {
            //Listor för spelets delar
            this.characters = new List<Character>();
            this.locations = new List<Location>();
            this.props = new List<Prop>();
            this.weapons = new List<Weapon>();

            

            //Character character1 = new Character("Leo");



            //Initiera spelets saker

            this.props.Add(new Prop("pedestal", "stor "));
            this.props.Add(new Prop("tron", "kunglig "));
            this.props.Add(new Prop("byggnader", "slitna "));

            this.props.Add(new Prop("grusväg", "en lång "));

            this.props.Add(new Prop("port", "en stor trä"));
            this.props.Add(new Prop("stenmurar", "gråa "));
            this.props.Add(new Prop("vattenpölar", "giftiga "));
            this.props.Add(new Prop("fladdermöss", "flaxande "));

            this.props.Add(new Prop("facklor", "tända "));
            this.props.Add(new Prop("fönster", "stora målade "));
            this.props.Add(new Prop("svart tron", "spikig "));



            //LÄGG TILL KARAKTÄRER
            this.characters.Add(new Character(0, "Leo", 16, 165, "blont hår", "tanig", rand.Next(5, 10), rand.Next(15, 20), 100));
            this.characters.Add(new Character(1, "Martin", 16, 170, "rött hår", "rund", rand.Next(2, 7), rand.Next(12, 17), 100));
            this.characters.Add(new Character(2, "Gwynndolf", 75, 180, "grått hår", "skranglig", rand.Next(7, 12), rand.Next(17, 22), 100));
            this.characters.Add(new Character(3, "Malakai", 200, 180, "svart hår", "muskulös", rand.Next(25, 35), rand.Next(40, 45), 250));
            this.characters.Add(new Character(4, "Sofia", 16, 160, "blont hår", "smal", 0, 0, 0));
            this.characters.Add(new Character(5, "Ork 1", 1, 180, "flint", "Grön", rand.Next(5, 10), rand.Next(15, 20), 100));
            this.characters.Add(new Character(6, "Ork 2", 1, 180, "flint", "Grön", rand.Next(5, 10), rand.Next(15, 20), 100));
            this.characters.Add(new Character(7, "Ork 3", 1, 180, "flint", "Grön", rand.Next(5, 10), rand.Next(15, 20), 100));



            //LÄGG TILL VAPEN
            this.weapons.Add(new Weapon("Heliga svärdet", "guld"));
            this.weapons.Add(new Weapon("stav", "kraftfulla"));

            //Lägg till Gammel torget som en plats och saker på platsen
            Location square = new Square("Gammel torget");
            square.AddProp(this.props.Find(x => x.Name == "pedestal"));
            square.AddProp(this.props.Find(x => x.Name == "tron"));
            square.AddProp(this.props.Find(x => x.Name == "byggnader"));
            this.locations.Add(square);


            //Lägg till fält som plats och saker på platsen
            Location field = new Field("fält");
            field.AddProp(this.props.Find(x => x.Name == "grusväg"));
            this.locations.Add(field);

            //Lägg till slottet som plats och saker på platsen
            Location castle = new Castle("slottet");
            castle.AddProp(this.props.Find(x => x.Name == "port"));
            castle.AddProp(this.props.Find(x => x.Name == "stenmurar"));
            castle.AddProp(this.props.Find(x => x.Name == "vattenpölar"));
            castle.AddProp(this.props.Find(x => x.Name == "fladdermöss"));
            this.locations.Add(castle);

            //Lägg till stort rum som plats och saker på platsen
            Location bigRoom = new BigRoom("stort rum");
            bigRoom.AddProp(this.props.Find(x => x.Name == "facklor"));
            bigRoom.AddProp(this.props.Find(x => x.Name == "fönster"));
            bigRoom.AddProp(this.props.Find(x => x.Name == "svart tron"));
            this.locations.Add(bigRoom);

            //STÄLL IN KARAKTÄRER
            this.mainCharacter = this.characters.Find(x => x.Name == "Leo");
            this.sideKick = this.characters.Find(x => x.Name == "Martin");
            this.princess = this.characters.Find(x => x.Name == "Sofia");
            this.mentor = this.characters.Find(x => x.Name == "Gwynndolf");
            this.villain = this.characters.Find(x => x.Name == "Malakai");
            this.ork1 = this.characters.Find(x => x.Name == "Ork 1");
            this.ork2 = this.characters.Find(x => x.Name == "Ork 2");
            this.ork3 = this.characters.Find(x => x.Name == "Ork 3");

            



            //STÄLL IN PLATSER
            //Ställ in startplats
            this.startingLocation = this.locations.Find(x => x.Name == "Gammel torget");
            //Ställ in första tröskelplats
            this.firstThresHoldLocation = this.locations.Find(x => x.Name == "fält");
            //Ställ in prövningsplats 1
            this.ordealLocation1 = this.locations.Find(x => x.Name == "slottet");
            //ställ in prövningsplats 2
            this.ordealLocation2 = this.locations.Find(x => x.Name == "stort rum");

            //STÄLL IN VAPEN
            //Ställ in svärd
            this.sword = this.weapons.Find(x => x.Name == "Heliga svärdet");
            this.staf = this.weapons.Find(x => x.Name == "stav");


            //Ställ in resans första steg
            this.stage = Stage.Beginning;
        }

        public void Run()
        {
            //Spelloop
            while (true)
            {
                /* Växla mellan resans steg
                 * Se sid. 9-20 i kursboken
                 */
                switch (stage)
                {
                    case Stage.Beginning:
                        //Hur förbereds resan?
                        UpdateBeginning();
                        break;
                    case Stage.FirstThreshold:
                        /* Resan har börjat, vad ska hända i detta steg?
                         * Hinder?
                         * Klmax?
                        */
                        UpdateFirstThreshold();
                        break;
                    case Stage.Ordeal:
                        /* Första delen av resan avslutas och lägger grunden för andra delen
                         * Vad ska hända i detta steg?
                         * Hinder?
                         * Klmax?
                        */
                        UpdareOrdeal();
                        break;
                    case Stage.TheRoadBack:
                        /* Vad ska hända i detta steg?
                         * Hinder?
                         * Klmax?
                        */
                        UpdateTheRoadBack();
                        break;
                    case Stage.Ending:
                        // Resans slut
                        UpdateEnding();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void UpdateBeginning()
        {
            Console.WriteLine("Updating beginning...");
            /***
             * Todo: fyll på med din kod
             ***/
            Console.Write("Berättelsen tar sin början vid " + startingLocation.Name + " i ett rike som heter Fala." +"\n");
            startingLocation.Present();
            Console.WriteLine(" för idag ska rikets beskyddare utses.");
            Console.WriteLine("Alla 16 åriga pojkar i staden har samlats för att försöka dra upp det " + sword.Name + " som sitter fast i pedestalen.");
            Console.WriteLine("En av dessa pojkar är " + mainCharacter.Name + ", han är " + mainCharacter.Height + "cm lång och " + mainCharacter.BodyType + " med " + mainCharacter.Haircolor + ".");
            Console.WriteLine("Han vet att han är svagare än dem flesta men han vill ändå imponera på princessan " + princess.Name + " som sitter på den kungliga tronen och tittar på.");
            Console.WriteLine(princess.Name + " är den vackraste han sett. Hon är " + princess.Height + "cm lång, " + princess.BodyType + " och har " + princess.Haircolor + ".");
            Console.WriteLine("Bredvid henne står den mäktiga trollkarlen " + mentor.Name + " som har lovat att guida beskyddaren. Han är " + mentor.Height + "cm lång, " + mentor.Age + " år gammal och ser " + mentor.BodyType + " ut men han är mäktig med sin " + staf.Appearance + " " + staf.Name + ".");
            Console.WriteLine("");
            Console.WriteLine("Innan det är Leos tur ska kompisen " + sideKick.Name + " göra ett försök, han är " + sideKick.Height + "cm lång med " + sideKick.Haircolor + " och har " + sideKick.BodyType + " figur.");
            Console.WriteLine("Martin drar och skriker: -KOM IGEN LOSSNA!! Men inget händer.");
            Console.WriteLine(mainCharacter.Name + " kliver fram och tar ett djupt andetag, han lägger båda händerna på svärdet med slutna ögon och lyfter.");
            Console.WriteLine("När han öppnar ögonen håller han i det " + sword.Name + " och publiken blir alldeles tokig och jublar.");
            Console.WriteLine("Prinsessan " + princess.Name + " står och klappar och Leo som är fylld av förvåning kan inte sluta le.");
            Console.WriteLine(mentor.Name + " kommer fram staplande med sin " + staf.Name + " bra gjort min pojk, jag ska lära dig allt jag kan.");
            Console.WriteLine("");
            Console.WriteLine("*POOF!!!* Hördes plötsligt ett högt ljud och hela torget fylldes med rök.");
            Console.WriteLine("När röken lagt sig ser vi att prisessan är bunden och den onde trollkarlen " + villain.Name + " har uppenbarat sig.");
            Console.WriteLine("Han är Gwyndorfs ärkerival och är " + villain.Height + "cm lång, " + villain.BodyType + " kroppsbyggnad och har " + villain.Haircolor + ".");
            Console.WriteLine("Malakai: -Muhahaha om ni vill se henne vid liv igen är det bäst att ni överger riket till mig.");
            Console.WriteLine(villain.Name + " teleporteras iväg med prinsessan.");
            Console.WriteLine("Gwyndorf: Det är nu det gäller " + mainCharacter.Name + ", du måste rädda henne.");
            Console.WriteLine("Leo: Jag!? Men jag vet inte hur man gör. Du har inte hunnit lära mig än.");
            Console.WriteLine("Gwynndorf: Ingen fara jag kommer lära dig på vägen.");
            Console.WriteLine("Martin: Och jag följer med dig.");
            Console.WriteLine("");


            Console.Write("tryck på 1 för att fortsätta resan eller någon annan tangent för att stanna kvar i början ");

            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == '1'))
            {
                Console.WriteLine("\nChanging stage to 'first threshold'...");
                //this.stage = Stage.FirstThreshold;
                this.stage = Stage.FirstThreshold;
            }
            else
                this.stage = Stage.Beginning;

            //Slut exempelkod

        }

        private void UpdateFirstThreshold()
        {

            Console.WriteLine("Updating first threshold...");
            /***
             * Todo: fyll på med din kod
             ***/
            Console.WriteLine("");
            Console.WriteLine("Mellan staden och slottet finns ett " + firstThresHoldLocation.Name);
            firstThresHoldLocation.Present(); //Kallar på funktionen Present i field
            Console.WriteLine("De tre hjältarna springer på grusvägen mot slottet när de träffar på tre orker.");
            Console.WriteLine(mentor.Name + ": dem är skickade för att stoppa oss. Vi måste strida!");
            Console.WriteLine("*STRIDEN STARTAR*");
            Console.WriteLine("");


            int orkHP = ork1.Hp + ork2.Hp + ork3.Hp; //Summan av orkernas hp
            //Återställer teamets HP
            mainCharacter.Hp = 100;
            mentor.Hp = 100;
            sideKick.Hp = 100;
            int teamHP = mainCharacter.Hp + mentor.Hp + sideKick.Hp; //summan av de spelbara karaktärernas hp

            while (orkHP > 0 && teamHP > 0) // Loop som körs så länge orkerna och teamet lever
            {

                    if(mainCharacterMove && mainCharacter.Hp > 0) //Om det är denna karaktärens tur att attackera och den lever så körs koden innanför måsvingarna
                    {
                    label1: //Etikett som gör att koden kan hoppa till den här raden
                        Console.WriteLine("Vad ska " + mainCharacter.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                        ConsoleKeyInfo mainCharacterAttack = Console.ReadKey();
                        if (mainCharacterAttack.KeyChar == '1') //Om maincharacter väljer attack 1
                        {
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo mainCharacterChoise = Console.ReadKey();
                            if (mainCharacterChoise.KeyChar == '1' && ork1.Hp > 0) //Om maincharacter väljer att attackera ork1 och ork1 är vid liv körs koden
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + " takes damage");
                                ork1.Hp = ork1.Hp - mainCharacter.FastAttack; //Gör så att orken tar skada av maincharacter
                                if(ork1.Hp < 0) //Gör så att orkens hp inte kan bli mindre än noll
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                mainCharacterMove = false; //maincharacters tur är över
                                mentorCharacterMove = true; //Nu är det mentorns tur
                                continue; // hoppar ut ur if satsen och går till nästa if sats (Mentorns if satser).

                            }
                            else if (mainCharacterChoise.KeyChar == '1' && ork1.Hp <= 0) //Om maincharacter attackerar ork1 och ork1 är död
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label1; //Gör så att kodläsaren hoppar till raden där etiketten label1 finns.
                            }
                            


                            if (mainCharacterChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "takes damage");
                                ork2.Hp = ork2.Hp - mainCharacter.FastAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (mainCharacterChoise.KeyChar == '2' && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label1;
                            }


                            if (mainCharacterChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "takes damage");
                                ork3.Hp = ork3.Hp - mainCharacter.FastAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (mainCharacterChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label1;
                            }
                        }
                        

                        if (mainCharacterAttack.KeyChar == '2')
                        {
                            label2:
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo mainCharacterChoise = Console.ReadKey();
                            if (mainCharacterChoise.KeyChar == '1' && ork1.Hp > 0)
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + "takes damage");
                                ork1.Hp = ork1.Hp - mainCharacter.HeavyAttack;
                                if (ork1.Hp < 0)
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (mainCharacterChoise.KeyChar == '1' && ork1.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label2;
                            }

                            if (mainCharacterChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "takes damage");
                                ork2.Hp = ork2.Hp - mainCharacter.HeavyAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (mainCharacterChoise.KeyChar == '2' && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label2;
                            }

                            if (mainCharacterChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "takes damage");
                                ork3.Hp = ork3.Hp - mainCharacter.HeavyAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (mainCharacterChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label2;
                            }
                        }
                    }

                    //Mentorns del (funkar på ungefär samma sätt som maincharacter)
                    if (mentorCharacterMove && mentor.Hp > 0)
                    {
                        Console.WriteLine("Vad ska " + mentor.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                        ConsoleKeyInfo mentorCharacterAttack = Console.ReadKey();
                        if (mentorCharacterAttack.KeyChar == '1')
                        {
                            label3:
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo mentorCharacterChoise = Console.ReadKey();
                            if (mentorCharacterChoise.KeyChar == '1' && ork1.Hp > 0)
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + " takes damage");
                                ork1.Hp = ork1.Hp - mentor.FastAttack;
                                if (ork1.Hp < 0)
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == '1' && ork1.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label3;
                            }

                            
                            if (mentorCharacterChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "takes damage");
                                ork2.Hp = ork2.Hp - mentor.FastAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == 2 && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label3;
                            }

                            if (mentorCharacterChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "takes damage");
                                ork3.Hp = ork3.Hp - mentor.FastAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label3;
                            }
                        }

                        
                        else if (mentorCharacterAttack.KeyChar == '2')
                        {
                            label4:
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo mentorCharacterChoise = Console.ReadKey();
                            if (mentorCharacterChoise.KeyChar == '1' && ork1.Hp > 0)
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + "takes damage");
                                ork1.Hp = ork1.Hp - mentor.HeavyAttack;
                                if (ork1.Hp < 0)
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == '1' && ork1.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label4;
                            }


                            if (mentorCharacterChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "takes damage");
                                ork2.Hp = ork2.Hp - mentor.HeavyAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == '2' && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label4;
                            }

                            if (mentorCharacterChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "takes damage");
                                ork3.Hp = ork3.Hp - mentor.HeavyAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (mentorCharacterChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label4;
                            }
                        }
                    }



                    //SideKicks del (Funkar på ungefär samma sätt som maincharacter och mentor)
                    if (sideKickMove && sideKick.Hp > 0)
                    {
                        Console.WriteLine("Vad ska " + sideKick.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                        ConsoleKeyInfo sideKickAttack = Console.ReadKey();
                        if (sideKickAttack.KeyChar == '1')
                        {
                            label5:
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo sideKickChoise = Console.ReadKey();
                            if (sideKickChoise.KeyChar == '1' && ork1.Hp > 0)
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + " takes damage");
                                ork1.Hp = ork1.Hp - sideKick.FastAttack;
                                if (ork1.Hp < 0)
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                sideKickMove = false;

                                continue;
                            }
                            else if (sideKickChoise.KeyChar == '1' && ork1.Hp <= 0)
                            {
                                
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label5;
                            }


                            if (sideKickChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "takes damage");
                                ork2.Hp = ork2.Hp - sideKick.FastAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                            }
                            else if (sideKickChoise.KeyChar == '2' && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label5;
                            }


                            if (sideKickChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "takes damage");
                                ork3.Hp = ork3.Hp - sideKick.FastAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                            }
                            else if (sideKickChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label5;
                            }
                        }

                        else if (sideKickAttack.KeyChar == '2')
                        {
                            label6:
                            Console.WriteLine("Vem ska attackeras? 1 (" + ork1.Name + ") / 2 (" + ork2.Name + ") / 3 (" + ork3.Name + ")");
                            ConsoleKeyInfo sideKickChoise = Console.ReadKey();
                            if (sideKickChoise.KeyChar == '1' && ork1.Hp > 0)
                            {
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                                Console.WriteLine(ork1.Name + "tar skada");
                                ork1.Hp = ork1.Hp - sideKick.HeavyAttack;
                                if (ork1.Hp < 0)
                                {
                                    ork1.Hp = 0;
                                }
                                Console.WriteLine(ork1.Name + " HP: " + ork1.Hp);
                            }
                            else if (sideKickChoise.KeyChar == '1' && ork1.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label6;
                            }

                            if (sideKickChoise.KeyChar == '2' && ork2.Hp > 0)
                            {
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                                Console.WriteLine(ork2.Name + "tar skada");
                                ork2.Hp = ork2.Hp - sideKick.HeavyAttack;
                                if (ork2.Hp < 0)
                                {
                                    ork2.Hp = 0;
                                }
                                Console.WriteLine(ork2.Name + " HP: " + ork2.Hp);
                            }
                            else if (sideKickChoise.KeyChar == '2' && ork2.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label6;
                            }

                            if (sideKickChoise.KeyChar == '3' && ork3.Hp > 0)
                            {
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                                Console.WriteLine(ork3.Name + "tar skada");
                                ork3.Hp = ork3.Hp - sideKick.HeavyAttack;
                                if (ork3.Hp < 0)
                                {
                                    ork3.Hp = 0;
                                }
                                Console.WriteLine(ork3.Name + " HP: " + ork3.Hp);
                            }
                            else if (sideKickChoise.KeyChar == '3' && ork3.Hp <= 0)
                            {
                                Console.WriteLine("Fienden är död. Tryck på valfri knapp");
                                Console.ReadKey();
                                goto label6;
                            }
                        }
                    }
                    
                    //Orkernas tur att attackera
                    //ork1s del
                    //ork1 väljer en attack genom att slumpa mellan 1 och 2
                    var ork1Attack = rand.Next(1, 2);
                    if (ork1Attack == 1 && ork1.Hp > 0) //Om ork1 väljer attack 1 och ork1 lever körs koden
                    {
                        var index = rand.Next(0, 3); //variabel som slumpar ett tal. 0, 1 eller 2. Denna variabel avgör vilken karaktär i listan som ska fokuseras på
                        Console.WriteLine("");
                        Console.WriteLine("Ork1: attackerar " + characters[index].Name); //ork1 attackerar t.ex. karaktär nummer 0 i listan, och kallar på den karaktärens namn. Då blir det alltså Leo
                        Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                        characters[index].Hp = characters[index].Hp - ork1.FastAttack;
                    if (characters[index].Hp < 0)
                    {
                        characters[index].Hp = 0;
                    }
                    Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                        
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }
                    else if(ork1Attack == 2 && ork1.Hp > 0)
                    {
                        var index = rand.Next(0, 3);
                        Console.WriteLine("");
                        Console.WriteLine("Ork1: attackerar " + characters[index].Name);
                        Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                        characters[index].Hp = characters[index].Hp - ork1.HeavyAttack;
                    if(characters[index].Hp < 0)
                    {
                        characters[index].Hp = 0;
                    }
                        Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }

                    //ork2s del (funkar ungefär som ork1s)

                    var ork2Attack = rand.Next(1, 2);
                    if(ork2Attack == 1 && ork2.Hp > 0)
                    {
                        var index2 = rand.Next(0, 3);
                        Console.WriteLine("");
                        Console.WriteLine("Ork2: attackerar " + characters[index2].Name);
                        Console.WriteLine(characters[index2].Name + " HP: " + characters[index2].Hp);
                        characters[index2].Hp = characters[index2].Hp - ork1.FastAttack;
                    if (characters[index2].Hp < 0)
                    {
                        characters[index2].Hp = 0;
                    }
                    Console.WriteLine(characters[index2].Name + " HP: " + characters[index2].Hp);
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }
                    else if(ork2Attack == 2 && ork2.Hp > 0)
                    {
                        var index2 = rand.Next(0, 3);
                        Console.WriteLine("");
                        Console.WriteLine("Ork2: attackerar " + characters[index2].Name);
                        Console.WriteLine(characters[index2].Name + " HP: " + characters[index2].Hp);
                        characters[index2].Hp = characters[index2].Hp - ork1.HeavyAttack;
                    if (characters[index2].Hp < 0)
                    {
                        characters[index2].Hp = 0;
                    }
                    Console.WriteLine(characters[index2].Name + " HP: " + characters[index2].Hp);
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }

                    //ork3s del (funkar ungefär som ork1 och ork2s)
                    var ork3Attack = rand.Next(1, 2);
                    if(ork3Attack == 1 && ork3.Hp > 0)
                    {
                        var index3 = rand.Next(0, 3);
                        Console.WriteLine("");
                        Console.WriteLine("Ork3: attackerar " + characters[index3].Name);
                        Console.WriteLine(characters[index3].Name + " HP: " + characters[index3].Hp);
                        characters[index3].Hp = characters[index3].Hp - ork1.FastAttack;
                    if (characters[index3].Hp < 0)
                    {
                        characters[index3].Hp = 0;
                    }
                    Console.WriteLine(characters[index3].Name + " HP: " + characters[index3].Hp);
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }
                    else if(ork3Attack == 2 && ork3.Hp > 0)
                    {
                        var index3 = rand.Next(0, 3);
                        Console.WriteLine("");
                        Console.WriteLine("Ork3: attackerar " + characters[index3].Name);
                        Console.WriteLine(characters[index3].Name + " HP: " + characters[index3].Hp);
                        characters[index3].Hp = characters[index3].Hp - ork1.HeavyAttack;
                    if (characters[index3].Hp < 0)
                    {
                        characters[index3].Hp = 0;
                    }
                    Console.WriteLine(characters[index3].Name + " HP: " + characters[index3].Hp);
                        Console.WriteLine("");
                        Console.WriteLine("Tryck på Enter");
                        Console.ReadKey();
                    }

                    teamHP = mainCharacter.Hp + mentor.Hp + sideKick.Hp; //Summan av de spelbara karaktärernas hp efter en runda i striden
                    orkHP = ork1.Hp + ork2.Hp + ork3.Hp; //Summan av orkernas hp efter en runda i striden

                //Om orkerna fortfarnade lever körs koden
                if (orkHP > 0)
                    {
                    
                    Console.WriteLine("team HP: " + teamHP); //Skriver ut teamets nuvarande hp
                    Console.WriteLine("ork1 HP: " + ork1.Hp + ", ork2 HP: " + ork2.Hp + ", ork3 HP: " + ork3.Hp); // skriver ut orkernas nuvarande hp
                    if (mainCharacter.Hp > 0) //Om maincharacter lever är det maincharacters tur att attackera, annars är det de mentorns eller sideKicks tur. På så sätt blir det en loop
                    {
                        mainCharacterMove = true;
                    }  
                    else if (mentor.Hp > 0)
                    {
                        mentorCharacterMove = true;
                    }
                    else if(sideKick.Hp > 0)
                    {
                        sideKickMove = true;
                    }
                        
                    }
                    else //Om inga orker lever har teamet vunnit.
                    {
                    Console.WriteLine("Hjältarna har segrat");
                    }

                    if(teamHP <= 0) //Om teamet är dött är spelet över
                {
                    
                    Console.WriteLine("Hjältarna har förlorat" + "\n");
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Tryck på valfri knapp för att avsluta spelet");
                    Console.ReadKey();
                    Environment.Exit(0); //avslutar spelet
                }

                    
                    
            }

            Console.WriteLine("");
            Console.Write("tryck på 1 för att fortsätta resan eller någon annan tangent för att stanna kvar i början ");

            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == '1'))
            {
                Console.WriteLine("\nChanging stage to 'ordeal'...");
                this.stage = Stage.Ordeal;
            }
            else
                this.stage = Stage.FirstThreshold;
        }

        private void UpdareOrdeal()
        {
            Console.WriteLine("Updating ordeal...");
            /***
             * Todo: fyll på med din kod
             ***/

            Console.WriteLine("Hjältarna har kämpat sig igenom mängder av fiender och har tillslut anlänt till " + ordealLocation1.Name + "\n");
            ordealLocation1.Present();
            Console.WriteLine("Platsen är av en märklig anledning obevakad " + "\n");
            Console.WriteLine("Gwynndorf: -Han vet att hans undersåtar inte duger längre så han väntar på dig innanför dessa portar." + "\n");
            Console.WriteLine("Leo: *andas in djupt*" + "\n");
            Console.WriteLine("Efter att ha stigit in i " + ordealLocation1.Name + " och sprungit upp för många trappor, har hjältarna kommit in i ett " + ordealLocation2.Name + "\n");
            ordealLocation2.Present();
            Console.WriteLine("*POOF*" + "\n");
            Console.WriteLine(villain.Name + " uppenbarar sig på tronen och med sig har han princessan uppbunden" + "\n");
            Console.WriteLine(villain.Name + ": -Välkommna mina herrar, jag har väntat. " + "\n");
            Console.WriteLine(mainCharacter.Name + ": -släpp henne genast!" + "\n");
            Console.WriteLine(villain.Name + ": -Jag ska... så fort ni lämnar över riket till mig." + "\n");
            Console.WriteLine(sideKick.Name + ": -Glöm det!" + "\n");
            Console.WriteLine(mentor.Name + ": -Det är lönlöst att förhandla " + villain.Name + ", gör dig redo att bli besegrad." + "\n");
            Console.WriteLine(villain.Name + ": -Som ni vill, kom ann bara!" + "\n");
            Console.WriteLine("");

            Console.WriteLine("*STRIDEN STARTAR*" + "\n");

            //Den här delen i koden är en strid mot en boss. Den funkar på ungefär samma sätt som förra striden men nu finns det bara en fiende
            
            int teamHP = mainCharacter.Hp + mentor.Hp + sideKick.Hp;


            while (villain.Hp > 0 && teamHP > 0)
            {
                var index = rand.Next(0, 3);
                if (mainCharacterMove && mainCharacter.Hp > 0)
                {
               
                    Console.WriteLine("Vad ska " + mainCharacter.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                    Console.WriteLine(characters[index].Name + " " + characters[index].Hp);
                    ConsoleKeyInfo mainCharacterAttack = Console.ReadKey();
                    if (mainCharacterAttack.KeyChar == '1')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - mainCharacter.FastAttack;
                            if (villain.Hp < 0)
                            {
                                villain.Hp = 0;
                            }

                            if(teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp + "\n");
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (teamHP < 0)
                            {
                                continue;
                            }
                        }     
                    }


                    if (mainCharacterAttack.KeyChar == '2')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - mainCharacter.HeavyAttack;
                            if (villain.Hp <= 0)
                            {
                                villain.Hp = 0;
                            }

                            if (teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                mainCharacterMove = false;
                                mentorCharacterMove = true;
                                continue;
                            }
                            else if (teamHP < 0)
                            {
                                continue;
                            }
                        }
                    }
                }

                if (mentorCharacterMove && mentor.Hp > 0)
                {
                
                    Console.WriteLine("Vad ska " + mentor.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                    ConsoleKeyInfo mentorCharacterAttack = Console.ReadKey();
                    if (mentorCharacterAttack.KeyChar == '1')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - mentor.FastAttack;
                            if (villain.Hp <= 0)
                            {
                                villain.Hp = 0;
                            }

                            if (teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (teamHP < 0)
                            {
                                continue;
                            }

                        }
                    }


                    if (mentorCharacterAttack.KeyChar == '2')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - mentor.HeavyAttack;
                            if (villain.Hp < 0)
                            {
                                villain.Hp = 0;
                            }

                            if (teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                mentorCharacterMove = false;
                                sideKickMove = true;
                                continue;
                            }
                            else if (teamHP < 0)
                            {
                                continue;
                            }
                        }
                    }
                }



                if (sideKickMove && sideKick.Hp > 0)
                {
                    Console.WriteLine("Vad ska " + sideKick.Name + " göra? 1 (snabb attack) / 2 (tung attack)");
                    ConsoleKeyInfo sideKickAttack = Console.ReadKey();
                    if (sideKickAttack.KeyChar == '1')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - sideKick.FastAttack;
                            if(villain.Hp < 0)
                            {
                                villain.Hp = 0;
                            }

                            if (teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                sideKickMove = false;
                                continue;
                            }
                            else if (teamHP < 0)
                            {
                                continue;
                            }

                        }
                    }


                    if (sideKickAttack.KeyChar == '2')
                    {
                        if (villain.Hp > 0)
                        {
                            Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                            Console.WriteLine(villain.Name + " takes damage");
                            villain.Hp = villain.Hp - sideKick.HeavyAttack;
                            if (villain.Hp < 0)
                            {
                                villain.Hp = 0;
                            }

                            if (teamHP > 0)
                            {
                                Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                                Console.WriteLine("Tryck på valfri tangent" + "\n");
                                Console.ReadKey();
                                sideKickMove = false;
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                //Bossens del
                var VillainAttack = rand.Next(1, 2);
                if (VillainAttack == 1 && villain.Hp > 0 && characters[index].Hp > 0)
                {

                    Console.WriteLine("");
                    Console.WriteLine(villain.Name + ": attackerar " + characters[index].Name);
                    Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                    characters[index].Hp = characters[index].Hp - villain.FastAttack;
                    if (characters[index].Hp <= 0)
                    {
                        characters[index].Hp = 0;
                    }
                    Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                    Console.WriteLine("");
                    Console.WriteLine("Tryck på Enter");
                    Console.ReadKey();


                }
                else if (VillainAttack == 2 && villain.Hp > 0 && characters[index].Hp > 0)
                {
                    
                    Console.WriteLine("");
                    Console.WriteLine(villain.Name + ": attackerar " + characters[index].Name);
                    Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                    characters[index].Hp = characters[index].Hp - villain.HeavyAttack;
                    if (characters[index].Hp < 0)
                    {
                        characters[index].Hp = 0;
                    }
                    Console.WriteLine(characters[index].Name + " HP: " + characters[index].Hp);
                    Console.WriteLine("");
                    Console.WriteLine("Tryck på Enter");
                    Console.ReadKey();
                    
                }

                teamHP = mainCharacter.Hp + mentor.Hp + sideKick.Hp;
                villain.Hp = villain.Hp;

                //Om bossen och teamet lever körs koden innanför måsvingarna
                if (villain.Hp > 0 && teamHP > 0)
                {

                    Console.WriteLine("Team HP: " + teamHP);
                    Console.WriteLine(villain.Name + " HP: " + villain.Hp);
                    if (mainCharacter.Hp > 0)
                    {
                        mainCharacterMove = true;
                    }
                    else if (mentor.Hp > 0)
                    {
                        mentorCharacterMove = true;
                    }
                    else if (sideKick.Hp > 0)
                    {
                        sideKickMove = true;
                    }

                }
                //Om bossen lever men teamet är dött är spelet över
                else if (villain.Hp > 0 && teamHP <= 0)
                {

                    Console.WriteLine("Hjältarna har förlorat" + "\n");
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Tryck på valfri knapp för att avsluta spelet");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            }

            //Om ingen av ovanstående if satser aktiveras (alltså om bossen är död men teamet lever) är striden vunnen och nästa del i spelet kan köras.
            Console.WriteLine("Hjältarna har segrat!" + "\n");

            Console.Write("tryck på 1 för att fortsätta resan eller någon annan tangent för att stanna kvar i början ");
            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == '1'))
            {
                Console.WriteLine("Changing stage to 'the road back'...");
                this.stage = Stage.TheRoadBack;
            }
            else
                this.stage = Stage.Ordeal;
        }



        private void UpdateTheRoadBack()
        {
            Console.WriteLine("Updating the road back...");
            /***
             * Todo: fyll på med din kod
             ***/
            Console.WriteLine(villain.Name + ": *andas utmattat*, -Omöjligt, jag kan inte besegras!");
            Console.WriteLine("Det " + sword.Name + " börjar plötsligt lysa.");
            Console.WriteLine(mainCharacter.Name + " tittar förvånat.");
            Console.WriteLine(mainCharacter.Name + ": -Vad är det som händer?");
            Console.WriteLine(mentor.Name + ": -Det måste vara det " + sword.Name + "s sanna kraft. Enligt legenden ska dess sanna kraft framkallas för att skydda riket.");
            Console.WriteLine(sideKick.Name + ": Gör det " + mainCharacter.Name + " gör slut på det!");
            Console.WriteLine(mainCharacter.Name + " lyfter upp " + sword.Name + " och gör ett mäktig hugg neråt, vilket skapar en stark projektil av ljus som träffar " + villain.Name);
            Console.WriteLine(villain.Name + ": *lyfts upp från marken och fattar eld*");
            Console.WriteLine("-Jag förbannar er! Ni ska få för det här!!!");
            Console.WriteLine(villain.Name + ": *Disintegreras*" + "\n");
            Console.WriteLine(sideKick.Name + ": *Kramar Leo* -du gjorde det " + mainCharacter.Name + ", vi vann!");
            Console.WriteLine(mentor.Name + ": *ger en klapp på axeln* -bra gjort min pojk jag är så stolt över dig.");
            Console.WriteLine(mainCharacter.Name + ": -tack men jag hade aldrig klarat det utan er. Juste prinscessan!");
            Console.WriteLine(mainCharacter.Name + " springer fram till tronen där " + princess.Name + "ligger uppbunden. Han knyter sedan upp henne.");
            Console.WriteLine(princess.Name + ": *Kramar Leo*, Åh tack för att du kom, du är en hjälte, nej NI är alla hjältar." + "\n");
            Console.WriteLine("Plötsligt börjar " + ordealLocation1.Name + "att skaka");
            Console.WriteLine(princess.Name + " -Vad är det som händer!");
            Console.WriteLine(mentor.Name + ": " + villain.Name + " måste ha varit den enda som hållit ihop det här gamla " + ordealLocation1.Name + ", utan honom kommer det att kollapsa");
            Console.WriteLine(mainCharacter.Name + " -Vi sticker härifrån nu!" + "\n");
            Console.WriteLine("Våra hjältar springer genom gångarna och ner för trapporna medan stenar faller från taket.");
            Console.WriteLine("Tillslut kommer dem utanför porten och springer ifrån slottet.");
            Console.WriteLine("De vänder sig sedan om och ser " + ordealLocation1.Name + " falla till marken.");
            Console.WriteLine(sideKick.Name + ": -Det är över.");
            Console.WriteLine(mainCharacter.Name + ": -Vi är säkra.");
            Console.WriteLine("Alla kramar om varandra om och börjar sedan vandra tillbaka till riket" + "\n");

            Console.Write("tryck på 1 för att fortsätta resan eller någon annan tangent för att stanna kvar i början ");
            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == '1'))
            {
                Console.WriteLine("Changing stage to 'ending'...");
                this.stage = Stage.Ending;
            }
            else
                this.stage = Stage.TheRoadBack;

            
        }

        private void UpdateEnding()
        {
            Console.WriteLine("Updating ending...");
            /***
             * Todo: fyll på med din kod
             ***/
            Console.WriteLine("Två dagar senare" + "\n");
            Console.WriteLine("Folket har återigen samlats på " + startingLocation.Name);
            Console.WriteLine("Princessan " + princess.Name + " sitter på den kungliga tronen och ser förväntansfull ut." + "\n");
            Console.WriteLine("*Trumpeter börjar spelas* och sedan kliver hjältarna fram på ett led.");
            Console.WriteLine("De ställer sig framför princessan och börjar sedan buga");
            Console.WriteLine(princess.Name + ": -Invånare av Fala, vi har samlats här idag för att fira våra hjältar som räddat oss alla.");
            Console.WriteLine(princess.Name + ": -" + sideKick.Name + ", " + mentor.Name + " och såklart " + mainCharacter.Name + ".");
            Console.WriteLine(princess.Name + ": -För att fira dessa krigare utnämner jag nu alla tre till stadens beskyddare och döper denna dagen i deras ära.");
            Console.WriteLine("*Folket jublar*.");
            Console.WriteLine(princess.Name + ": -Från och med nu ska denna dagen firas så låt festen börja!" + "\n");
            Console.WriteLine("Festen varade i en vecka och alla levde i trygghet ända tills det var dags att utsé rikets nästa beskyddare" + "\n");
            Console.WriteLine("Slut" + "\n");

            Console.Write("tryck på 1 för att avsluta spelet eller någon annan tangent för att återgå till början ");
            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == '1'))
            {
                Environment.Exit(0);
            }
            else
            Console.WriteLine("Changing stage to 'beginning'...");
            this.stage = Stage.Beginning;
        }

    }
}
