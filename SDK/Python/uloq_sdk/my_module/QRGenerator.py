
import asyncio
from typing import Optional
from .connectors import HttpConnector
from .models import QRCodeRequest, QRCodeResponse, ConnectionModel, OutcomeObject

class QRGenerator:
    def __init__(self, connection_model: ConnectionModel):
        self._connection_model = connection_model

    async def generate_qr_code(self, request: QRCodeRequest) -> Optional[QRCodeResponse]:
        http_connector = HttpConnector(self._connection_model)
        result = await http_connector.post_async("createqrcode", request.to_json())
        
        if result.success:
            return result.result
        return None
