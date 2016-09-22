using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace printernet
{
  public class orderCXML
  {
    public string xmlFileName, orderID, custFName, custLName, custFullName, custAddress1, custAddress2,
      custAddress3, custCity, custState, custZip, custFullPhone, custAreaCode, custCountryCode, custPhone, shipMethod;
    public void createXml(string orderXmlPath, string orderID)
    {
      //ritzOrder order = new ritzOrder(orderXmlPath);
      try{
        //get general order info

        XDocument appConfigXml = XDocument.Load(configFiles.configXml);
        var sharedSecret = appConfigXml.Descendants("shared_secret").ElementAt(0).Value;
        var ftpServerUrl = appConfigXml.Descendants("ftp_server").ElementAt(0).Value;
        var orderSubmitUrl = appConfigXml.Descendants("order_submit_url").ElementAt(0).Value;
        var ordersFolder = appConfigXml.Descendants("orders_folder").ElementAt(0).Value;
        var cXmlSaveFolder = appConfigXml.Descendants("cxml_folder").ElementAt(0).Value;

        XDocument orderXml = XDocument.Load(orderXmlPath);

        var orderDate = orderXml.Element("apm_order").Attribute("timestamp").Value;
        var surface = orderXml.Descendants("attributes").ElementAt(0).Attribute("finish").Value;
        if (surface == "glossy")
        {
          surface = "Gloss";
        }
        //var grand_total = orderXml.Element("apm_order").Attribute("grand_total").Value;

        var qryCustomerInfo = from customerInfo in orderXml.Descendants("shipment")
                              select customerInfo;
        foreach (XElement customer in qryCustomerInfo)
        {
          custFName = customer.Attribute("fname").Value;
          custLName = customer.Attribute("lname").Value;
          custAddress1 = customer.Attribute("address1").Value;
          if (customer.Attribute("address2") != null)
          {
          custAddress2 = customer.Attribute("address2").Value;
          }
          if (customer.Attribute("address3") != null)
          {
            custAddress3 = customer.Attribute("address3").Value;
          }
          custCity = customer.Attribute("city").Value;
          custState = customer.Attribute("state").Value;
          custZip = customer.Attribute("zip").Value;
          custFullPhone = customer.Attribute("phone").Value;
          shipMethod = customer.Attribute("delivery_method").Value;
        }

        custFullName = custFName + " " + custLName;
        //string[] phonePieces = custFullPhone.Split(' ');
        //if (custFullPhone.IndexOf("+") > 0)
        //{
        //  custPhone = phonePieces[2];
        //  custAreaCode = phonePieces[1];
        //  custCountryCode = phonePieces[0];
        //}
        //else
        //{
        //  custPhone = phonePieces[1];
        //  custAreaCode = phonePieces[0];
        //  custCountryCode = "";
        //}
        //custAreaCode = custAreaCode.Replace(")", "");
        //custAreaCode = custAreaCode.Replace("(", "");
        xmlFileName = orderXmlPath.Split('\\').Last();
        //orderID = xmlFileName.Replace(".xml", "");
        
        string orderFolder = orderID + ".done";
        //string doneFolderPath = Path.Combine(ordersFolder, "done");
        string orderFolderPath = Path.Combine(ordersFolder, orderFolder);
        
        string oldOrderFolder = orderID + ".order";
        string oldOrderFolderPath = Path.Combine(ordersFolder, oldOrderFolder);

        //string ftpOrderFolder = Path.Combine(ftpServerUrl, orderFolder);

        //create the order cxml
        XDocument cXml = new XDocument(
          new XElement("cXML",
              new XAttribute("version", "1.2.005"),
              new XAttribute(XNamespace.Xml + "lang", "en-US"),
              new XAttribute("payloadID", orderID + "@camarketing.com"),
              new XAttribute("timestamp", DateTime.Now),
              new XElement("Header",
                new XElement("From",
                  new XElement("Credential",
                    new XAttribute("domain", ""),
                    new XElement("Identity", "")),
                  new XElement("Credential",
                    new XAttribute("domain", ""),
                    new XElement("Identity", ""))),
                new XElement("To",
                  new XElement("Credential",
                    new XAttribute("domain", ""),
                    new XElement("Identity", ""))),
                new XElement("Sender",
                  new XElement("Credential",
                    new XAttribute("domain", ""),
                    new XElement("Identity", ""),
                    new XElement("SharedSecret", sharedSecret)))),
              new XElement("Request",
                new XAttribute("deploymentMode", "production"),
                new XElement("OrderRequest",
                  new XElement("OrderRequestHeader",
                    new XAttribute("orderID", orderID),
                    new XAttribute("orderDate", orderDate),
                    new XAttribute("type", "24HR"),
                    new XElement("BillTo",
                      new XElement("Address",
                        new XAttribute("addressID", ""),
                        new XElement("Name", "C A Marketing",
                          new XAttribute(XNamespace.Xml + "lang", "en-US")),
                        new XElement("PostalAddress", 
                          new XAttribute("name", ""),
                          new XElement("DeliverTo", "Attn: Billing"),
                          new XElement("Street", "2 Bergen Turnpike"),
                          new XElement("Street", ""),
                          new XElement("City", "Ridgefield Park"),
                          new XElement("State", "NJ"),
                          new XElement("PostalCode", "07660"),
                          new XElement("Country", "US",
                            new XAttribute("isoCountryCode", "US"))),
                        new XElement("Phone",
                          new XElement("TelephoneNumber",
                            new XElement("CountryCode",
                              new XAttribute("isoCountryCode", "")),
                            new XElement("AreaOrCityCode", "201"),
                            new XElement("Number", "881-1900"))))),
                    new XElement("Shipping", 
                      new XElement("Money",
                        new XAttribute("currency", "USD")),
                      new XElement("Description",
                        new XAttribute(XNamespace.Xml + "lang", "en-US"))),
                    new XElement("Tax",
                      new XElement("Money", 
                        new XAttribute("currency", "USD")),
                      new XElement("Description",
                        new XAttribute(XNamespace.Xml + "lang", "en-US"))),
                    new XElement("Comments",
                      new XAttribute(XNamespace.Xml + "lang", "en-US")))))));


        var qryOrderXml = from orderInfo in orderXml.Descendants("order_item")
                          select orderInfo;
        int itemCounter = 1;

        foreach (XElement orderSection in qryOrderXml){
          string productID, quantity, description, fileName, originalName, borders, line_item_total;

          bool for_fulfillment = true;
          if(orderSection.Attribute("for_fulfillment") != null){
            string fulfillment = orderSection.Attribute("for_fulfillment").Value;
            if (fulfillment == "false")
            {
              for_fulfillment = false;
            }
          }

          if (for_fulfillment == true)
          {
            productID = orderSection.Attribute("product").Value;
            quantity = orderSection.Attribute("quantity").Value;
            description = orderSection.Attribute("name").Value;
            fileName = orderSection.Element("image").Attribute("path").Value;
            originalName = orderSection.Element("image").Attribute("original_name").Value;
            borders = orderSection.Element("image").Attribute("borders").Value;
            line_item_total = orderSection.Attribute("line_item_total").Value;

            Uri baseUrl = new Uri(ftpServerUrl);
            string orderFile = Path.Combine(orderFolder, fileName);
            var imageUrl = new Uri(baseUrl, orderFile).ToString();

            cXml.Element("cXML").Element("Request").Element("OrderRequest").Add(
              new XElement("ItemOut",
                new XAttribute("lineNumber", itemCounter),
                new XAttribute("quantity", quantity),
                new XAttribute("requestedDeliveryDate", ""),
                new XElement("ItemID",
                  new XElement("SupplierPartID", productID),
                  new XElement("SupplierPartAuxiliaryID", "")),
                new XElement("ItemDetail",
                  new XElement("UnitPrice",
                    new XElement("Money", line_item_total,
                      new XAttribute("currency", "USD"))),
                  new XElement("Description", description,
                    new XAttribute(XNamespace.Xml + "lang", "en-US")),
                  new XElement("UnitOfMeasure", ""),
                  new XElement("Classification",
                    new XAttribute("domain", "")),
                  new XElement("URL", imageUrl),
                  new XElement("Extrinsic", "item" + itemCounter,
                    new XAttribute("name", "lineItemID")),
                  new XElement("Extrinsic", "1",
                    new XAttribute("name", "quantityMultiplier")),
                  new XElement("Extrinsic", "1",
                    new XAttribute("name", "Pages")),
                  new XElement("Extrinsic", orderID,
                    new XAttribute("name", "endCustomerOrderID")),
                  new XElement("Extrinsic", originalName,
                    new XAttribute("name", "OriginalName")),
                  new XElement("Extrinsic", shipMethod,
                    new XAttribute("name", "requestedShipper")),
                  new XElement("Extrinsic", "886A73",
                    new XAttribute("name", "requestedShippingAccount")),
                  new XElement("Extrinsic", "",
                    new XAttribute("name", "costCenter")),
                  new XElement("Extrinsic", borders,
                    new XAttribute("name", "Borders")),
                  new XElement("Extrinsic", surface,
                    new XAttribute("name", "Surface"))),
                new XElement("ShipTo",
                  new XElement("Address",
                    new XAttribute("addressID", "1"),
                    new XElement("Name",
                      new XAttribute(XNamespace.Xml + "lang", "en-US")),
                    new XElement("PostalAddress",
                      new XAttribute("name", ""),
                      new XElement("DeliverTo", custFullName),
                      new XElement("Street", custAddress1),
                      new XElement("Street", custAddress2),
                      new XElement("Street", custAddress3),
                      new XElement("City", custCity),
                      new XElement("State", custState),
                      new XElement("PostalCode", custZip),
                      new XElement("Country", "US",
                        new XAttribute("isoCountryCode", "US"))),
                    new XElement("Phone",
                      new XElement("TelephoneNumber",
                        new XElement("CountryCode", "",
                          new XAttribute("isoCountryCode", "")),
                        new XElement("AreaOrCityCode", ""),
                        new XElement("Number", custFullPhone)))))));
            itemCounter++;
          }
        }

        if (!Directory.Exists(cXmlSaveFolder))
        {
          Directory.CreateDirectory(Path.Combine(ordersFolder, "cXmls"));
          cXmlSaveFolder = Path.Combine(ordersFolder, "cXmls");
        }
        string cXmlFilePath = Path.Combine(cXmlSaveFolder, "ET_" + xmlFileName);
        cXml.Save(cXmlFilePath);
        logIt logEntry = new logIt(orderID, "Successfully Created cXml order file.");
        Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback("Successfully created cXml order file.", Color.Black, orderID); });
        //addUserFeedback("Successfully created cXml order file.", Color.Black, orderID);

        //need to move this out of try, errors for transmitCxml are being caught here instead in that function.
        transmitCxml(cXmlFilePath, orderSubmitUrl, orderXmlPath, orderID);
        renameFolder(orderID, oldOrderFolderPath, orderFolderPath);
          //Directory.Move(oldOrderFolderPath, orderFolderPath);
          //Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback("Renamed folder to " + orderFolderPath, Color.Black, orderID); });
          //logIt logEntry2 = new logIt(orderID, "Renamed folder to " + orderFolderPath);
      }
      catch (Exception em)
      {
        logIt logEntry = new logIt(orderID, em.ToString(), "", 1);
        Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback(em.ToString(), Color.Red, orderID); });
        string orderPath = Path.GetDirectoryName(orderXmlPath);
        Directory.Move(orderPath, orderPath.Replace("order", "ERROR"));
      }

    }

    private void renameFolder(string orderID, string oldName, string newName)
    {
        Directory.Move(oldName, newName);
        Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback("Renamed folder to " + newName, Color.Black, orderID); });
        logIt logEntry = new logIt(orderID, "Renamed folder to " + newName);
    }

    public void transmitCxml(string cXmlFilePath, string orderSubmitUrl, string orderXmlpath, string orderID)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(orderSubmitUrl);

      StreamReader reader = new StreamReader(cXmlFilePath);
      string postData = reader.ReadToEnd();
      reader.Close();

      Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback("Sending cXml...", Color.Black, orderID); });

      byte[] payload = Encoding.UTF8.GetBytes(postData);
      request.ContentType = "text/xml";
      request.ContentLength = payload.Length;
      request.Method = "POST";

      Stream dataStream = request.GetRequestStream();
      dataStream.Write(payload, 0, payload.Length);
      dataStream.Close();

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      if (response.StatusCode == HttpStatusCode.OK)
      {
       // Stream responseStream = response.GetResponseStream();

       try
        {
          XDocument responseXml = XDocument.Load(response.GetResponseStream());
          var responseCode = responseXml.Descendants("Status").ElementAt(0).Attribute("code").Value;
          var responseText = responseXml.Descendants("Status").ElementAt(0).Attribute("text").Value;
          //string responseCode = "unknown";
          //string responseText = responseXml.ToString();

          logIt logEntry = new logIt(orderID, responseText, responseXml.ToString(), Int32.Parse(responseCode));

          var statusColor = Color.Black;
          if (responseCode != "200")
          {
            statusColor = Color.Red;
          }
          //Form1 addSuccessFeedback = new Form1();
          //Form1._Form1.addUserFeedback(responseCode + " - " + responseText, statusColor, orderID);
          Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback(responseCode + " - " + responseText, statusColor, orderID); });
        }
        catch(Exception e)
        {
          logIt logEntry = new logIt(orderID, "cXml was successfully transmitted but no XML response was received.  Message: " + e.ToString(), "", 2);
          Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback("cXml was successfully transmitted but no XML response was received.  Message: " + e.ToString(), Color.Red, orderID); });
          string orderFolderPath = Path.GetDirectoryName(orderXmlpath);
          Directory.Move(orderFolderPath, orderFolderPath.Replace("order", "ERROR"));
        }
      }
      else
      {
        string message = "Could not connect to " + orderSubmitUrl + ".  Response: ";
        logIt logEntry = new logIt(orderID, message + response.StatusCode.ToString(), "", (int)response.StatusCode);
        Form1._Form1.Invoke((MethodInvoker)delegate { Form1._Form1.addUserFeedback(message, Color.Red, orderID); });
        string orderFolderPath = Path.GetDirectoryName(orderXmlpath);
        Directory.Move(orderFolderPath, orderFolderPath.Replace("order", "ERROR"));
      }
    }


  }
}
