# eTravel – Recommendation-Based Travel Application

## 📌 Introduction

**eTravel** je full-stack aplikacija za turističke usluge koja omogućava rezervaciju ponuda, hotela i soba, uz integrisan **sistem preporuke** baziran na mašinskom učenju (ML.NET).  
Projekat uključuje **backend servise**, **desktop i mobilnu Flutter aplikaciju**, kao i **Docker infrastrukturu** za jednostavno pokretanje.

Ovaj README fajl objašnjava:
- potrebne tehnologije
- način pokretanja projekta
- testne korisničke podatke
- opcije za testiranje aplikacije (desktop i mobile)

---

## 🛠️ Tehnologije i alati

Za provjeru i pokretanje projekta potrebno je imati instalirano:

- **Git**
- **Docker & Docker Compose**
- **Visual Studio (2022 ili noviji)**
- **Android Studio**
- **Flutter SDK**
- **.NET SDK (za backend, ako se ručno pokreće)**

---

## 📥 Kloniranje projekta

Projekat se preuzima sa GitHub repozitorija pomoću sljedeće komande:


git clone <GITHUB_REPO_LINK>



## 🔐 Konfiguracija (VAŽNO)

⚠️ **NAPOMENA**

Nakon `git clone`, u repozitoriju se nalazi **šifrirani fajl**: 

konfiguracija-etravel.zip

### Koraci:

1. Otvoriti šifrirani fajl `konfiguracija-etravel.zip`
2. Iz njega izvaditi fajl **`.env`**
3. **Prije pokretanja Dockera**, `.env` fajl ubaciti u **root folder projekta**  
   (folder gdje je urađen `git clone`)

⚠️ **Bez ovog koraka Docker servisi se neće pravilno pokrenuti.**

---

## 🐳 Pokretanje Dockera

Kada je `.env` fajl pravilno postavljen, u terminalu (root folder projekta) pokrenuti:


docker compose up -d --build




## ▶️ Pokretanje aplikacije

Postoje **dva načina** za pokretanje i testiranje aplikacije.

---

### 🔹 Način 1: Korištenje gotovih build fajlova (PREPORUČENO)

U projektu se nalazi **šifrirani fajl**:
fit-build-2026-01-13.zip

🔐 **Šifra arhive:** `fit`

Unutar arhive se nalaze:
- **Release/** – `.exe` fajl za pokretanje **desktop aplikacije**
- **flutter-apk/** – `.apk` fajl za testiranje **mobilne aplikacije**

Ovo je **najbrži način** za testiranje aplikacije bez dodatne konfiguracije.

---

### 🔹 Način 2: Pokretanje iz izvornog koda

#### 🖥️ Desktop aplikacija

1. Otvoriti folder `etravel_desktop`
2. U terminalu pokrenuti:


flutter run -d windows

#### 📱 Mobilna aplikacija (Android)

1. Otvoriti folder `etravel_mobile`
2. Pokrenuti **Android Emulator** ili povezati fizički Android uređaj
3. U terminalu pokrenuti:


flutter run


## 🧪 Testni korisnički podaci

### 🖥️ Desktop aplikacija

**Radnik**
- Username: `radnik`
- Password: `Radnik123!`

**Direktor**
- Username: `direktor`
- Password: `Direktor123!`

---

### 📱 Mobilna aplikacija

**Korisnik**
- Username: `edinmesic55`
- Password: `Korisnik123!`


## 💳 PayPal – Testni podaci (Sandbox)

Za testiranje PayPal plaćanja koristiti sljedeći **sandbox nalog**:

- **Email:** `TestniPrimjerPersonal@gmail.com`
- **Password:** `Korisnik123!`

## Email testiranje

Za testiranje dolaska maila na email dummy korisnika
"Edin Mešic" koristite:

- **Email:** `edinmesic5566@gmail.com`
- **Password:** `edinMesic123

NAPOMENA 

Molim Vas koristite ove podatke jer oporavak lozinke radi
na principu pronalaska maila koji je u registrovanim korisnicima
`







