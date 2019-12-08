import KEY
from Crypto.Util import number
from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_OAEP
from base64 import b64decode, b64encode

# Genera una llave privada
def RSAconstructorPrivate(modulus, exponent, d, p, q, inverseQ):
    modulus_encode = number.bytes_to_long(b64decode(modulus))
    exponent_encode = number.bytes_to_long(b64decode(exponent))
    d_encode = number.bytes_to_long(b64decode(d))
    p_encode = number.bytes_to_long(b64decode(p))
    q_encode = number.bytes_to_long(b64decode(q))
    inverseQ_encode = number.bytes_to_long(b64decode(inverseQ))
    return RSA.construct((modulus_encode, exponent_encode, d_encode, p_encode, q_encode, inverseQ_encode))

# Genera una llave privada desde el archivo KEY
def RSAmyPrivateKey():
    modulus_encode = number.bytes_to_long(b64decode(KEY.Modulo))
    exponent_encode = number.bytes_to_long(b64decode(KEY.Exponent))
    d_encode = number.bytes_to_long(b64decode(KEY.D))
    p_encode = number.bytes_to_long(b64decode(KEY.P))
    q_encode = number.bytes_to_long(b64decode(KEY.Q))
    inverseQ_encode = number.bytes_to_long(b64decode(KEY.InverseQ))
    return RSA.construct((modulus_encode, exponent_encode, d_encode, p_encode, q_encode, inverseQ_encode))

# Genera una llave publica
def RSAconstructorPublic(modulus, exponent):
    modulus_encode = number.bytes_to_long(b64decode(modulus))
    exponent_encode = number.bytes_to_long(b64decode(exponent))
    return RSA.construct((modulus_encode, exponent_encode))

# Encripta un string con una llave publica
def RSAencrypt(string_msg, publicKey):
    cipher = PKCS1_OAEP.new(publicKey)
    minsize = 0
    maxsize = 200
    encode_msg = ""
    while len(string_msg) > maxsize:
        encode_msg += b64encode(cipher.encrypt(string_msg[minsize:maxsize].encode())).decode()
        minsize = maxsize
        maxsize += 200
    encode_msg += b64encode(cipher.encrypt(string_msg[minsize:len(string_msg)].encode())).decode()
    return encode_msg

# Desencripta un mensaje con la llave privada del servidor
def RSAdecrypt(encrypt_msg, privateKey):
    cipher = PKCS1_OAEP.new(privateKey)
    minsize = 0
    maxsize = 344
    decode_msg = ""
    while len(encrypt_msg) > maxsize:
        decode_msg += cipher.decrypt(b64decode(encrypt_msg[minsize:maxsize].encode())).decode()
        minsize = maxsize
        maxsize += 344
    decode_msg += cipher.decrypt(b64decode(encrypt_msg[minsize:len(encrypt_msg)].encode())).decode()
    return decode_msg

# Encripta el string especificado con la clave publica definida en KEY.py
def myEncrypt(string_msg):
    private_key = RSAconstructorPrivate(KEY.Modulo, KEY.Exponent, KEY.D, KEY.P, KEY.Q, KEY.InverseQ)
    public_key = private_key.publickey()
    publicKey = public_key.exportKey('DER')
    publicKey = b64encode(publicKey).decode()
    public_key = RSA.importKey(b64decode(publicKey))
    return RSAencrypt(string_msg, public_key)

# Encripta el string especificado con una llave publica externa
def myEncryptExternalPublicKey(string_msg, string_modulus, string_exponent):
    private_key = RSAconstructorPublic(string_modulus, string_exponent)
    public_key = private_key.publickey()
    publicKey = public_key.exportKey('DER')
    publicKey = b64encode(publicKey).decode()
    public_key = RSA.importKey(b64decode(publicKey))
    return RSAencrypt(string_msg, public_key)

# Desencripta el string especificado con la clave privada definida en KEY.py
def myDecrypt(string_msg, private_key):
    privateKey = private_key.exportKey('DER')
    privateKey = b64encode(privateKey).decode()
    private_key = RSA.importKey(b64decode(privateKey))
    return RSAdecrypt(string_msg, private_key)

#Recibiendo un mensaje
# encrypt_msg = "a4vQbCUrYBq/xVKtHPfAPmeZ13Hf2vBaNCdxpDsJTDsCrLhJakORvvUC+JjRz4S9CvB1RgRW2VLBCJLmbqb+jB7T82Hf83fNObAnumBvW6LjVZnDhvgBdAd4A0IuTko6F7MK/Z5iHU5xrRh0Vm96Pbl5yRNCbamTM9kXNpxCRi2rSZuW7YoSuUD0iStg88OY9615EqJiLV/jipsRre4TTqxGC8JHNrTGKGjg2v1WenI4ZW1E+LsuQc+/KFxCyL+KnCU++j1iVKpsUwfDYx8SXpV606GCWTg3Y5H6oynB8i4xBaol1V3b/SlUzSzJtYQACqCL2HlBVzlofOJX3vMhHg==DktVQpp5O5I4jtiqISYq1FQVbzOqgtr6+lcZqbKlx70/MnzNcrmNlLVzTfDCIanrVOeXQ6KNVVKT6d4Tn0gRAf4+rZcTgGzXZe5Ami0X1ismVyq7EAUL21Nyhh3DmX5LY+ptZotX63AkHJPP9oXNLZKl54JcUozmAoH7Z15Ps6QNTZXkkgF/XGWUtWcDoDMbtrNA3neEXl0AZJQqKHdSrnfbgEYUg/vynwPBlUxOzHH2eRq9x6PzqbwO/sPAhKXmFp2qD8l25Ai0gGlTyjNKn5Xawy1oFfLNrlPCQwIWKjdwLEdGmOcMOPiuRIrRstcxT8zhinEp9rVPGynjn488yw==g3+96Ek/5Tpn7HUkfoKsUiJsVeO/7d26MtoPew84P+nR9XRS7tsZC980Fi7+gL5cFIzTS78FYzRVwtfFMPu+MIc6Lr72aX1GOXsF7oARs7boYU/Ued3YwJMjpT85rdGuvrggw0PR5MIMyXF37xkvVkEAYToReZ2De4fyU4p062jhZSLjTcSXBNeZxNdOMIvvCN7FmiAbHTtner2A3oHFXwS+YcqbdVZrFgxXXG2oQaeSbvZ7gIjOEvPftWOQijCzUcN/pLG1lcwft5enKx80M2EIK14k//GKSt4SQu0SJ56DXFVudVL+tzuUpGVrbK8oqFZQN5/3awP6BV5JwmYwyg=="
# decrypt_msg = myDecrypt(encrypt_msg, RSAmyPrivateKey())
# print(decrypt_msg)

#Encryptando json publica de key
# x = '{"username":"pepe",	"password":"hola hola hola","rank2":"C","rank1":"",	"rank3":""}'
# encrypt_msg = myEncrypt(x)
# print(encrypt_msg)

#Enviando un mensaje
# msg = "hola caracola"
# modulus = "gPRm356AUfKdAKB6QC8fOIS9QdEHBinTPpHcgFHYqSXoiJTN8BAKuOHIgqaSL4/vKuwpPXq4P4bIgmmBwr6LZ3aqaT75Rprwu/6c8YEz/cJisQyJMeIB+hqsJTPtLT2ehfaDHFb9tZmpcRyGZGKBNLEafqSr4ndbupPG+nmAVrZXCVKXSKJQsr0jfvzc82Rb9rXMvr1vgFFwC91ntOBqvsWXCZw9BHmH4B/8g1YgqoCjK8yjdIklVaP4T5Wj8vKby9Y9HaTvmzbk63C6w4Vs8ZFRkJmVQ0+4XZVT5JYPXUSAu3Pvdi/60Aa7aOBVS+rjhKAzsv6+NOdStSHivzMCYQ=="
# exponent = "AQAB"
# encrypt_msg = myEncryptExternalPublicKey(msg, modulus, exponent)
# print(encrypt_msg)

#Encriptando y desencriptando un mensaje interno
# private_key = RSAconstructorPrivate(KEY.Modulo, KEY.Exponent, KEY.D, KEY.P, KEY.Q, KEY.InverseQ)
# public_key = private_key.publickey()
#
# privateKey = private_key.exportKey('DER')
# publicKey = public_key.exportKey('DER')
#
# privateKey = str(b64encode(privateKey))[2:len(str(b64encode(privateKey)))-1]
# publicKey = str(b64encode(publicKey))[2:len(str(b64encode(publicKey)))-1]
#
# print("PrivateKey: " + privateKey)
# print("PublicKey: " + publicKey)
#
# private_key = RSA.importKey(b64decode(privateKey))
# public_key = RSA.importKey(b64decode(publicKey))
#
# msg = "holaholahoaholaholaholaholaholaholaholaholaholaholaholahoaholaholaholaholaholaholaholaholaholaholaholahoaholaholaholaholaholaholaholaholaholaholaholahoaholaholaholahoaholaholaholaholaholaholaholaholaholaholaholahoahola"
# print(msg)
#
# encrypt_msg = RSAencrypt(msg, public_key)
# print(encrypt_msg)
#
# decrypt_msg = RSAdecrypt(encrypt_msg, private_key)
# print(decrypt_msg)