
from typing import Optional

class ConnectionModel:
    def __init__(self, api_key: str, api_secret: str, base_url: str, proxy_url: Optional[str] = None):
        self.api_key = api_key
        self.api_secret = api_secret
        self.base_url = base_url
        self.proxy_url = proxy_url
