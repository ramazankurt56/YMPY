# Redis Kurulumu

## Windows i�in
- Denetim masas� Program Ekle kald�rdan Windows �zellikleri A� Kapat men�s�nden Windows Subsystem For Linux se�ene�ini i�aretle
- Microsoft Store Ubuntu Program�n� kur
- A�a��daki kodlar� s�rayla �al��t�r	
** sudo apt update
** sudo apt install redis-server
** redis-server
** redis-cli pin //�al���p �al��mad���n� kontrol i�in. E�er �al���yorsa PONG cevab� gelir

Not: �stersek buradaki repodan direkt bir install ile kurabiliyoruz: https://github.com/tporadowski/redis/releases

## Docker ile
docker run --name redis-cache -p 6379:6379 -d redis

**  docker exec -it redis-cache redis-cli ping  //�al���p �al��mad���n� kontrol i�in. E�er �al���yorsa PONG cevab� gelir