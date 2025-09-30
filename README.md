# Introduction 
#  BalkanStay

**BalkanStay** je web aplikacija koja omogućava **pretragu i rezervaciju apartmana na području Balkana**.  
Aplikacija je izrađena u tehnologijama:
- **Backend**: .NET 8 Web API
- **Frontend**: Angular (CLI)


##  Pokretanje aplikacije

Nakon što klonirate projekat na svoj računar, pratite korake ispod kako biste pokrenuli **backend** i **frontend** aplikaciju.

---

###  Backend (.NET 8 Web API – IIS Express)

#### Alati potrebni:
- Visual Studio 2022 ili noviji
- .NET SDK 8.0+
- SQL Server (lokalni)

#### Koraci:

1. **Otvori rješenje u Visual Studio.**
2. **U Tools meniju odaberi:**
   - `Build > Rebuild Solution`
3. **Postavi `BalkanStay.Api` kao Startup Project.**
4. **Otvori Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).**
5. Pokreni sljedeće komande u konzoli:

```powershell
Add-Migration Init
Update-Database
. 


#### Za pokretanje Angular projekta, otvori terminal u frontend folderu i pokreni komande:

npm install
ng serve


**Testni nalozi se kreiraju automatski prilikom prvog pokretanja migracija.**

Uloga	            Username	Email	             Lozinka
****************************************************************
Administrator	    adilAdmin	  —	                 password
Korisnik	        adilUser	adil+1@edu.fit.ba	 password



Ime i prezime: Izel Repuh

Godina studija: 3. godina

Predmet: RS1 – Razvoj softvera 1

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
