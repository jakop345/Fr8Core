using System.Collections.Generic;

using DocuSign.Integrations.Client;

using Data.Interfaces.DataTransferObjects;

namespace Data.Interfaces
{
    public interface IEnvelope
    {
        /// <summary>
        /// Get Envelope Data from a docusign envelope. 
        /// Each EnvelopeData row is essentially a specific DocuSign "Tab".
        /// </summary>
        /// <param name="envelope">DocuSign.Integrations.Client.Envelope envelope domain.</param>
        /// <returns>
        /// List of Envelope Data.
        /// It returns empty list of envelope data if tab and signers not found.
        /// </returns>
        List<EnvelopeDataDTO> GetEnvelopeData(Envelope envelope);
        

        /// <summary>
        /// Get Envelope Data from a docusign envelope. 
        /// Each EnvelopeData row is essentially a specific DocuSign "Tab".
        /// </summary>
        /// <param name="templateId">templateId</param>
        /// <returns>
        /// List of Envelope Data.
        /// It returns empty list of envelope data if tab and signers not found.
        /// </returns>
        IEnumerable<EnvelopeDataDTO> GetEnvelopeData(string templateId);
    }
}