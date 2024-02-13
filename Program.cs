using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Parallel_Plinq
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("people.json");
            List<Usuari> _users = JsonConvert.DeserializeObject<List<Usuari>>(json);

            ListView lvJSON = new ListView();

            foreach (Usuari _user in _users)
            {
                ListViewItem _list = new ListViewItem(_user.Name);
                _list.SubItems.Add(_user.Dni);
                _list.SubItems.Add("true o false");
                lvJSON.Items.Add(_list);
            }
            ComprovaSeqüencial(_users);

            ComprovaParallel(_users);

            Console.ReadLine();
        }

        static void ComprovaSeqüencial(List<Usuari> usuaris)
        {
            var startTime = DateTime.Now;

            foreach (Usuari usuari in usuaris)
            {
                bool dniCorrecte = usuari.ComprovaDNI();
                bool nomCorrecte = usuari.ComprovaNom();
                bool mailCorrecte = usuari.ComprovaMail();

                Console.WriteLine($"Seqüencial - Nom: {usuari.Name}, DNI Correcte: {dniCorrecte}, Nombre Correcte: {nomCorrecte}, Mail Correcte: {mailCorrecte}");
            }
            Console.WriteLine($"Temps Seqüencial: {(DateTime.Now - startTime).TotalMilliseconds} ms");
        }
        
        static void ComprovaParallel(List<Usuari> usuaris)
        {
            var startTime = DateTime.Now;

            Parallel.ForEach(usuaris, usuari =>
            {
                bool dniCorrecte = usuari.ComprovaDNI();
                bool nomCorrecte = usuari.ComprovaNom();
                bool mailCorrecte = usuari.ComprovaMail();

                Console.WriteLine($"Paral·lel - Nom: {usuari.Name}, DNI Correcte: {dniCorrecte}, Nombre Correcte: {nomCorrecte}, Mail Correcte: {mailCorrecte}");
            });

            Console.WriteLine($"Temps Paral·lel: {(DateTime.Now - startTime).TotalMilliseconds} ms");
        }
    }
}
