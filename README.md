# Before the Blast 🎮  

## 1. Proje Tanımı  
Bu proje, Unity oyun motoru ile geliştirilmiş bir aksiyon/strateji oyunudur.  
**Amaç:** Bomba patlamadan önce düşmanları etkisiz hale getirip bombayı durdurmak.  
(Oyunun temel mantığı: düşmanları temizle → bomba konumuna ulaş → bombayı devre dışı bırak.)

## 2. Sistem Şeması  
Oyunun genel mimarisi şu bileşenleri içerir:  
- **Sahne Yönetimi:** MainMenu → GameScene → Settings  
- **Oyun Mantığı:** PlayerController, EnemyAI, BombController  
- **UI Yönetimi:** HUD (can, puan, zamanlayıcı)  
- **Veri/Asset Yönetimi:** Addressables veya Resources sistemi  


## 3. Oyun Mekanikleri – Blok Diyagram  
Temel akış:  
**Başlangıç (Menü) → Oyun başlat → Düşman spawn → Çatışma → Bombaya ulaş → Bombayı durdur veya patlama**  
FlowChart.png de detaylı hali mevcut

## 4. Tasarlanan Sahneler  
Projede bulunan başlıca sahneler:  
- **Giriş Menüsü (Main Menu):** Oyuna başla, ayarlar, çıkış seçenekleri.  
- **Oyun Sahnesi (Game Scene):** Ana oynanış alanı — oyuncu kontrolü, düşmanlar, bomba, HUD.  
- **Ayarlar (Settings):** Ses ayarları, zorluk seviyesi değişimi.  

## 5. Literatür Taraması ve Esinlenme  
Bu proje, **Half-Life** serisindeki görev temelli ilerleyiş ve ortam atmosferinden esinlenilmiştir.  
Half-Life’ta oyuncu, bilimsel bir tesiste yaşanan patlama sonrasında düşmanlara karşı savaşırken, zaman baskısı altında hayatta kalmaya ve görevleri tamamlamaya çalışır.  
Bu yapı, oyuncuya hem aksiyon hem de stratejik düşünme becerilerini bir arada kullanma imkânı sunar.  

Bizim projemizde ise bu konsept sadeleştirilerek, **bomba patlamadan önce düşmanları etkisiz hale getirip bombayı durdurma** üzerine odaklanılmıştır.  
Half-Life’taki görev atmosferi, çevresel gerilim ve zaman baskısı duygusu referans alınarak tasarlanmıştır.  
Oyundaki yapay zekâ sistemi ileri seviye davranış modelleri yerine, **Unity NavMesh** kullanılarak basitleştirilmiş bir şekilde uygulanmıştır.  
Bu sayede oyuncu, tıpkı Half-Life’ta olduğu gibi çevresel farkındalık, hareket ve zaman yönetimi unsurlarını bir arada deneyimlemektedir.


## 6. Yazılımsal Mimari, Yöntemler ve Teknikler  
Projede kullanılan başlıca teknikler:  
- **Unity (C#)** tabanlı geliştirme  
- **NavMesh:** Düşman hareketleri ve yol bulma  
- **Animator/Spine:** Karakter animasyonları  
- **Canvas Scaler:** Farklı çözünürlüklere uyumlu arayüz  
- **GitHub:** Sürüm kontrolü ve ekip işbirliği  

## 7. Karşılaşılan Zorluklar ve Çözümler  
- **NavMesh Sorunları:** Düşmanların konum algılamasında hatalar vardı. Çözüm olarak NavMesh yüzey parametreleri (radius, height, avoidance) yeniden düzenlendi.  
- **Spine Animasyon Problemleri:** İlk denemede animasyon bozuktu. Export ve import ayarları düzeltilerek yeniden oluşturuldu.  
- **Ekranlar Arası Geçiş:** Sahne geçişlerinde veri kaybı yaşandı. Çözüm: `SceneManager.LoadSceneAsync` ve `DontDestroyOnLoad` kullanımı.  
- **Sürüm Çatışmaları:** Git çatışmaları için branch’leme ve düzenli merge süreçleri uygulandı.

## 8. Projenin Kattıkları  
- Unity ile oyun geliştirme sürecine hâkimiyet  
- Asset kullanımı, import/export yönetimi  
- NavMesh ve AI temelleri  
- GitHub üzerinden sürüm kontrolü  
- Takım çalışması ve görev paylaşımı deneyimi  

## 9. Kurulum & Çalıştırma  
1. Bu repoyu klonlayın:  
   ```bash
   git clone https://github.com/Emirhanfiliz/Untiy_project.git
