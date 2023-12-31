/* 
 * Uloq Requestor Service
 *
 * Requestor Endpoints
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Uloq.SDK.Eccenscia.Services.Models.OpenAPIDateConverter;

namespace Uloq.SDK.Eccenscia.Services.Models.UloqRequestor
{
    /// <summary>
    /// NotificationDetailsResponse
    /// </summary>
    [DataContract]
    public partial class NotificationDetailsResponse : IEquatable<NotificationDetailsResponse>, IValidatableObject
    {
        /// <summary>
        /// Defines Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Approved for value: Approved
            /// </summary>
            [EnumMember(Value = "Approved")]
            Approved = 1,

            /// <summary>
            /// Enum Declined for value: Declined
            /// </summary>
            [EnumMember(Value = "Declined")]
            Declined = 2

        }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "Status", EmitDefaultValue = false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDetailsResponse" /> class.
        /// </summary>
        /// <param name="keyIdentifier">Base64 string.</param>
        /// <param name="notificationIdentifier">notificationIdentifier.</param>
        /// <param name="status">status.</param>
        /// <param name="payload">payload.</param>
        /// <param name="signature">Base64 representation of the transaction signature. Order - KeyIdentifier, NotificationIdentifier, Status, Payload (assending order).</param>
        /// <param name="publicKey">publicKey.</param>
        /// <param name="identifierMetadata">Base64 representation of the metadata associated with the identifier.</param>
        public NotificationDetailsResponse(string keyIdentifier = default, string notificationIdentifier = default, StatusEnum? status = default, List<PayloadObject> payload = default, string signature = default, string publicKey = default, string identifierMetadata = default)
        {
            KeyIdentifier = keyIdentifier;
            NotificationIdentifier = notificationIdentifier;
            Status = status;
            Payload = payload;
            Signature = signature;
            PublicKey = publicKey;
            IdentifierMetadata = identifierMetadata;
        }

        /// <summary>
        /// Base64 string
        /// </summary>
        /// <value>Base64 string</value>
        [DataMember(Name = "KeyIdentifier", EmitDefaultValue = false)]
        public string KeyIdentifier { get; set; }

        /// <summary>
        /// Gets or Sets NotificationIdentifier
        /// </summary>
        [DataMember(Name = "NotificationIdentifier", EmitDefaultValue = false)]
        public string NotificationIdentifier { get; set; }

        /// <summary>
        /// Gets or Sets Payload
        /// </summary>
        [DataMember(Name = "Payload", EmitDefaultValue = false)]
        public List<PayloadObject> Payload { get; set; }

        /// <summary>
        /// Base64 representation of the transaction signature. Order - KeyIdentifier, NotificationIdentifier, Status, Payload (assending order)
        /// </summary>
        /// <value>Base64 representation of the transaction signature. Order - KeyIdentifier, NotificationIdentifier, Status, Payload (assending order)</value>
        [DataMember(Name = "Signature", EmitDefaultValue = false)]
        public string Signature { get; set; }

        /// <summary>
        /// Gets or Sets PublicKey
        /// </summary>
        [DataMember(Name = "PublicKey", EmitDefaultValue = false)]
        public string PublicKey { get; set; }

        /// <summary>
        /// Base64 representation of the metadata associated with the identifier
        /// </summary>
        /// <value>Base64 representation of the metadata associated with the identifier</value>
        [DataMember(Name = "IdentifierMetadata", EmitDefaultValue = false)]
        public string IdentifierMetadata { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NotificationDetailsResponse {\n");
            sb.Append("  KeyIdentifier: ").Append(KeyIdentifier).Append("\n");
            sb.Append("  NotificationIdentifier: ").Append(NotificationIdentifier).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Payload: ").Append(Payload).Append("\n");
            sb.Append("  Signature: ").Append(Signature).Append("\n");
            sb.Append("  PublicKey: ").Append(PublicKey).Append("\n");
            sb.Append("  IdentifierMetadata: ").Append(IdentifierMetadata).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as NotificationDetailsResponse);
        }

        /// <summary>
        /// Returns true if NotificationDetailsResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of NotificationDetailsResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NotificationDetailsResponse input)
        {
            if (input == null)
                return false;

            return
                (
                    KeyIdentifier == input.KeyIdentifier ||
                    KeyIdentifier != null &&
                    KeyIdentifier.Equals(input.KeyIdentifier)
                ) &&
                (
                    NotificationIdentifier == input.NotificationIdentifier ||
                    NotificationIdentifier != null &&
                    NotificationIdentifier.Equals(input.NotificationIdentifier)
                ) &&
                (
                    Status == input.Status ||
                    Status.Equals(input.Status)
                ) &&
                (
                    Payload == input.Payload ||
                    Payload != null &&
                    input.Payload != null &&
                    Payload.SequenceEqual(input.Payload)
                ) &&
                (
                    Signature == input.Signature ||
                    Signature != null &&
                    Signature.Equals(input.Signature)
                ) &&
                (
                    PublicKey == input.PublicKey ||
                    PublicKey != null &&
                    PublicKey.Equals(input.PublicKey)
                ) &&
                (
                    IdentifierMetadata == input.IdentifierMetadata ||
                    IdentifierMetadata != null &&
                    IdentifierMetadata.Equals(input.IdentifierMetadata)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (KeyIdentifier != null)
                    hashCode = hashCode * 59 + KeyIdentifier.GetHashCode();
                if (NotificationIdentifier != null)
                    hashCode = hashCode * 59 + NotificationIdentifier.GetHashCode();
                hashCode = hashCode * 59 + Status.GetHashCode();
                if (Payload != null)
                    hashCode = hashCode * 59 + Payload.GetHashCode();
                if (Signature != null)
                    hashCode = hashCode * 59 + Signature.GetHashCode();
                if (PublicKey != null)
                    hashCode = hashCode * 59 + PublicKey.GetHashCode();
                if (IdentifierMetadata != null)
                    hashCode = hashCode * 59 + IdentifierMetadata.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
