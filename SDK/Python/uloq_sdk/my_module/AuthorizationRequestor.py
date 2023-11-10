
import asyncio
import json
from datetime import datetime, timedelta
from typing import Optional
from .connectors import HttpConnector
from .Eccenscia.Services.Models.UloqRequestor import AuthorizationRequest, AuthorizationResponse, OutcomeObject, NotificationDetailsRequest
from .models import ConnectionModel

class AuthorizationRequestor:
    def __init__(self, connection_model: ConnectionModel):
        self._connection_model = connection_model

    async def create_authorization(self, key_identifier, notification_identifier, expiry_date, category, action_title, action_message, metadata):
        authorization_request = AuthorizationRequest(
            key_identifier=key_identifier,
            notification_identifier=notification_identifier,
            action_message=action_message,
            action_title=action_title,
            category=category,
            expiry_date_utc=expiry_date.utcnow().isoformat(),
            metadata=metadata
        )
        return await self._create_authorization(authorization_request)

    async def _create_authorization(self, authorization_request: AuthorizationRequest):
        http_connector = HttpConnector(self._connection_model)
        outcome_object = await http_connector.post_async("requestauthorization", json.dumps(authorization_request))
        return outcome_object.success

    async def get_authorization_response(self, notification_details_request: NotificationDetailsRequest):
        http_connector = HttpConnector(self._connection_model)
        outcome_object = await http_connector.post_async("gettransactionresult", json.dumps(notification_details_request))
        if outcome_object.result and outcome_object.success:
            return outcome_object.result
        return None

    async def run_authorization_response_task(self, notification_details_request: NotificationDetailsRequest, interval: timedelta, timeout: timedelta):
        end_time = datetime.now() + timeout

        while datetime.now() < end_time:
            response = await self.get_authorization_response(notification_details_request)
            if response:
                return response
            await asyncio.sleep(interval.total_seconds())

        return None  # Timed out
