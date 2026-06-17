# 🚀 YUDI (MENDY ADMIN) | IDENTITY & MESSAGING SYSTEM
Bu proje, ASP.NET Core 10.0 kullanılarak geliştirilmiş, gelişmiş kullanıcı yönetim sistemine (Identity) ve gerçek zamanlı e-posta doğrulama mekanizmasına sahip modern bir yönetim panelidir. 
Kullanıcı deneyimini ön planda tutan AJAX tabanlı doğrulamalar ve kurumsal seviyede bir mesajlaşma altyapısı sunar.


## 🛠️ KULLANILAN TEKNOLOJİLER
* 💻 **Backend:** #ASP.NET Core 8.0 (MVC)<br>
* 🔐 **Güvenlik:** #Microsoft Identity & #Two-Factor Authentication (2FA)<br>
* 📧 **E-Posta:** #MailKit & #MimeKit SMTP Integration<br>
* 💾 **Veritabanı:** #MSSQL Server & #Entity Framework Core<br>
* ⚡ **Frontend:** #AJAX, #jQuery, #Bootstrap 5<br>


### 1. E-Mail Doğrulama Ekranı (Authentication & Security)
<img src="Screenshots/Ekran görüntüsü 2026-06-18 015023.png" alt="E-Mail Verification Screen" width="100%"/>

* **Fonksiyon:** Kullanıcı kayıt veya giriş aşamasında güvenlik amacıyla tetiklenen 6 haneli OTP (One-Time Password) doğrulama adımı.
* **Backend & SQL Mantığı:** * Kullanıcı talep oluşturduğunda backend (`.NET 8.0`) tarafında benzersiz ve süreli (örn. 3-5 dk geçerli) 6 haneli bir kod üretilir.
  * Bu kod, veritabanındaki `UserVerifications` veya `Users` tablosunda ilgili kullanıcı ID'si, kodun kendisi ve `ExpirationDate` alanları ile saklanır. Aynı zamanda `MailKit/MimeKit` üzerinden kullanıcının e-posta adresine gönderilir.
  * Kullanıcı kodu girdiğinde `.NET` tarafındaki endpoint veritabanından güncel kodu çeker; kodun doğruluğunu ve süresinin geçip geçmediğini SQL üzerinde veya backend mantığında doğrular. Başarılı ise `IsVerified` durumu `true` çekilir.
* **Frontend & UX:** `Tailwind CSS` ile ortalanmış temiz bir beyaz kart yapısı kullanılmıştır. Giriş alanları için `focus` efektleri içeren esnek `flex` yapıda tasarlanmış kutular ve gradyan (`bg-gradient-to-r from-purple-400 to-pink-500`) buton mimarisi uygulanmıştır.

---

### 2. Ana Kontrol Paneli & Dashboard (Aktivite Özeti)
<img src="Screenshots/Ekran görüntüsü 2026-06-18 014556.png" alt="Dashboard Screen" width="100%"/>

* **Fonksiyon:** Kullanıcının genel durumunu, profil tamamlanma oranını ve son 12 günün mesaj trafiğini grafiksel olarak gördüğü merkez üssü.
* **Backend & SQL Mantığı:**
  * **Smart Statistics:** Toplam gönderilen mesaj sayısı ve etkileşim kurulan kişi sayısı, SQL'deki `Messages` tablosundan `COUNT(Id)` ve `COUNT(DISTINCT ReceiverId)` gibi agregasyon (kümeleme) sorgularıyla anlık olarak çekilir.
  * **Data Visualization (Aktivite Özeti):** `.NET` backend tarafında, son 12 günü kapsayacak şekilde `GROUP BY CAST(CreatedDate AS DATE)` sorgusu çalıştırılarak her güne düşen mesaj sayıları bir dizi (array) halinde frontend'e beslenir.
* **Frontend & UX:** `grid grid-cols-1 md:grid-cols-3` responsive grid mimarisiyle kutuların konumlandırılması yapılmıştır. Grafik barları için veritabanından gelen sayısal oranlara göre dinamik yükseklik alan Tailwind tabanlı özel bar tasarımları kullanılmıştır.

---

### 3. Profil Görünümü ve Düzenleme Modalı (User Profile Management)
<img src="Screenshots/Ekran görüntüsü 2026-06-18 014549.png" alt="User Profile Screen" width="100%"/>

* **Fonksiyon:** Kullanıcının unvan, şirket, şehir, web sitesi ve "Hakkında" yazısı gibi kişisel bilgilerini listelediği ve modal (açılır pencere) üzerinde güncelleyebildiği alan.
* **Backend & SQL Mantığı:**
  * Profil bilgileri SQL'deki `Users` veya `UserProfiles` tablosunda saklanır. 
  * "Kaydet" butonuna basıldığında `.NET` backend'e bir `UpdateProfileDto` nesnesi gönderilir. Backend tarafında veri validasyonundan (doğrulama) geçtikten sonra Entity Framework Core kullanılarak `UPDATE Users` sorgusu güvenli bir şekilde çalıştırılır ve `Identity User Context` ile tam senkronize kaydedilir.
* **Frontend & UX:** Modal yapısı için `fixed inset-0 bg-black bg-opacity-40` ile arka planı karartma ve `z-50` katman yönetimi kullanılmıştır. Form inputları için modern ve yumuşak kenarlı form elemanları tercih edilmiştir.

---

### 4. Gelen Kutusu ve Taslaklar Listeleme (Data Table & Filtering)
<img src="Screenshots/Ekran görüntüsü 2026-06-18 014540.png" alt="Inbox Screen" width="100%"/>

* **Fonksiyon:** Kullanıcıya gelen veya kullanıcının yarıda bıraktığı taslak mesajların listelendiği, kategori etiketlerine göre ayrıştırıldığı liste ekranları.
* **Backend & SQL Mantığı:**
  * `Messages` tablosunda `Status` (Gelen, Taslak, Gönderilmiş, Çöp Kutusu) ve `Category` (İş, Okul, Seyahat, Finans) alanları bulunur.
  * Sol menüdeki klasör sayıları, veritabanından `COUNT` sorgularıyla dinamik olarak hesaplanıp listelenir.
  * Filtreleme butonuna basıldığında veya klasörler arasında geçiş yapıldığında, `.NET` tarafına parametreler (örn: `?status=Draft`) gönderilir ve SQL tarafında `WHERE Status = 'Draft' AND UserId = @CurrentUserId` şartıyla veriler optimize bir şekilde LINQ sorgularıyla sayfalanarak (Pagination) getirilir.
* **Frontend & UX:** Liste elemanlarının `flex justify-between items-center hover:bg-gray-50 transition` özellikleri ile satır satır ayrılması sağlanmıştır. Kategoriler için dinamik Tailwind renk sınıfları (Örn: İş için mor `bg-purple-100 text-purple-700`) kullanılmıştır.

---

### 5. Yeni Mesaj Oluşturma & Gelişmiş Editör (Compose Mail)
<img src="Screenshots/Ekran görüntüsü 2026-06-18 014531.png" alt="Compose Mail Screen" width="100%"/>

* **Fonksiyon:** Alıcı seçimi, kategori belirleme, zengin metin editörü (Rich Text Editor) ile içerik hazırlama ve sürükle-bırak (Drag & Drop) mantığıyla dosya ekleme alanı.
* **Backend & SQL Mantığı:**
  * **Smart Recipient Management:** "Alıcı" alanına isim yazıldıkça arka planda bir arama endpoint'i tetiklenir ve SQL'de `WHERE Name LIKE '%aranan_kelime%'` sorgusuyla eşleşen kullanıcılar anlık dökülür.
  * **Taslak Kaydet:** Kullanıcı metni yazarken veriler `Messages` tablosuna `IsDraft = 1` olacak şekilde kaydedilir. "Mesajı Gönder" dendiğinde ise bu durum güncellenir ve alıcının gelen kutusuna düşer.
  * **Dosya Ekleme:** Yüklenen dosyalar `.NET` backend tarafında `IFormFile` entegrasyonu ile sunucu tarafında güvenli bir şekilde saklanır; SQL veritabanındaki `Attachments` tablosuna ise dosya yolu (`FilePath`) kaydedilerek ilgili mesajla (`MessageId`) ilişkilendirilir.
* **Frontend & UX:** Dosya yükleme alanı için kesikli kenarlıklara sahip `border-2 border-dashed border-gray-300 rounded-xl hover:border-purple-500` yapısı kullanılmıştır. Metin editörü araç çubuğu ve form alanları tek bir sayfaya sığacak şekilde, dikey kaydırma çubuklarına (scrollbar) boğulmadan minimalist tasarlanmıştır.

---

## 🚀 VERİ TUTARLILIĞI VE MİMARİ NOTLAR

* **Soft Delete Sistemi:** Veritabanında (SQL) verilerin kalıcı olarak silinmesi yerine `IsDeleted` ve `IsSpam` gibi flag'lerle işaretlenerek klasörler arası mantıksal taşınması sağlanır.
* **Identity Integration:** Her mesaj, klasör ve profil işlemi, o an oturum açmış olan `User.Identity.Name` bilgisine göre backend tarafında sıkı bir filtrelemeden geçirilerek güvenliği sağlanır.
* **Asenkron Yapı (AJAX):** Sayfa yenilemelerini (No-Reload) engellemek amacıyla form gönderimleri ve filtreleme işlemleri `e-preventDefault();` mimarisiyle tamamen AJAX tabanlı kurgulanmıştır.Reload) engellemek amacıyla form gönderimleri ve filtreleme işlemleri `e-preventDefault();` mimarisiyle tamamen AJAX tabanlı kurgulanmıştır.
