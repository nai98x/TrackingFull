using iTextSharp.text;
using iTextSharp.text.pdf;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC.Report
{
    public class PaqueteReport
    {
        #region Declaration
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(4);
        MemoryStream _memoryStream = new MemoryStream();
        Paquete _paq;
        string _estado, _origen, _destino;
        iTextSharp.text.Image _imgQR;
        #endregion

        public byte[] PrepararReport(Paquete paq, string estado, string origen, string destino, iTextSharp.text.Image imgQRCode)
        {
            _paq = paq;
            _estado = estado;
            _origen = origen;
            _destino = destino;
            _imgQR = imgQRCode;

            #region
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _document.AddTitle("Reporte de Paquete " + paq.Id);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 50f, 50f, 50f, 50f });
            #endregion

            this.CrearContenido();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }

        private void CrearContenido()
        {
            _document.Add(new Paragraph("Paquete"));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Datos:"));
            _document.Add(new Paragraph("    - Descripcion: " + _paq.Descripcion));
            _document.Add(new Paragraph("    - Estado: " + _estado));
            _document.Add(new Paragraph("    - Origen: " + _origen));
            _document.Add(new Paragraph("    - Destino: " + _destino));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Remitente:"));
            _document.Add(new Paragraph("    - Email: " + _paq.Remitente.Email));
            _document.Add(new Paragraph("    - Nombre: " + _paq.Remitente.Nombre));
            _document.Add(new Paragraph("    - Direccion: " + _paq.Remitente.Direccion));
            _document.Add(new Paragraph("    - Telefono: " + _paq.Remitente.Telefono));
            _document.Add(new Paragraph("    - Tipo de Documento: " + _paq.Remitente.TipoDocumento));
            _document.Add(new Paragraph("    - Nro de Documento: " + _paq.Remitente.NroDocumento));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Destinatario:"));
            _document.Add(new Paragraph("    - Email: " + _paq.Destinatario.Email));
            _document.Add(new Paragraph("    - Nombre: " + _paq.Destinatario.Nombre));
            _document.Add(new Paragraph("    - Direccion: " + _paq.Destinatario.Direccion));
            _document.Add(new Paragraph("    - Telefono: " + _paq.Destinatario.Telefono));
            _document.Add(Chunk.NEWLINE);
            _document.Add(_imgQR);
        }
    }
}