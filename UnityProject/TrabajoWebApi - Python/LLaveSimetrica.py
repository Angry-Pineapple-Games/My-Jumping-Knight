from Crypto.Cipher import AES
from base64 import b64decode, b64encode


def encrypt(key, raw, iv, bs):
    raw = _pad(b64encode(raw.encode()).decode(), bs)
    cipher = AES.new(key, AES.MODE_CBC, iv)
    return b64encode(iv + cipher.encrypt(raw))

def decrypt(key, enc, iv):
    enc = b64decode(enc)
    cipher = AES.new(key, AES.MODE_CBC, iv)
    result = _unpad(cipher.decrypt(enc[AES.block_size:])).decode('utf-8')
    return b64decode(result.encode()).decode()

def _unpad(s):
    return s[:-ord(s[len(s)-1:])]

def _pad(s, bs):
    return s + (bs - len(s) % bs) * chr(bs - len(s) % bs)

#example
# import KEY
# msg = "holañ"
# key = KEY.KEYSECRET
# print(key)
# iv = KEY.IV
# bs = KEY.BS
#
#
# encrypt_msg = encrypt(key, msg, iv, bs)
# encrypt_msg = str(encrypt_msg)[2:len(str(encrypt_msg))-1]
# print(encrypt_msg)
#
# decrypt_msg = decrypt(key, encrypt_msg, iv)
# print(decrypt_msg)

#example 2
# import hashlib
# from Crypto import Random
# key = "EstaEsMiClaveSecreta"
# msg = "bien"
#
# key = hashlib.sha256(key.encode()).digest()
# print("key: " + str(key))
# myKey = str(b64encode(key))[2:len(str(b64encode(key)))-1]
# print("myKey: " + myKey)
# mynewKey = b64decode(myKey)
# print("myNewKey: " + str(mynewKey))
#
# iv = Random.new().read(AES.block_size)
# print("Iv: " + str(iv))
# myIv = str(b64encode(iv))[2:len(b64encode(iv))-1]
# print("myIv: " + str(myIv))
#
# enc = encrypt(key, msg, iv, 16)
# print("Msg: " + str(enc))
# myEnc = str(enc)[2:len(b64encode(enc))-1]
# print("myMsg: " + str(myEnc))
# sol = decrypt(key, enc, iv)
# print("Msg: " + str(sol))