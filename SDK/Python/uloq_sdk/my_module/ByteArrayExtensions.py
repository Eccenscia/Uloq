
import base64

def to_base64(byte_array: bytes) -> str:
    return base64.b64encode(byte_array).decode('utf-8')

def from_base64(encoded_str: str) -> bytes:
    return base64.b64decode(encoded_str)
