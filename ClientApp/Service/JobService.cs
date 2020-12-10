﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ClientApp.Common;
using ClientApp.Models;
using Newtonsoft.Json;

namespace ClientApp.Service
{
    public class JobService
    {
        public ServiceEntity Post(ServiceEntity ser)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PostAsync(PropertiesFile.HOST + "Service/Post/", new StringContent(
                    new JavaScriptSerializer().Serialize(ser), Encoding.UTF8, "application/json")).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    return ser;
            }
            return null;
        }
        public string Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(PropertiesFile.HOST + "Service/Delete/" + id);
                var responseTask = client.DeleteAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {


                    return result.Content.ReadAsStringAsync().Result; // nếu return ngay đây sao k return lại method trên luôn
                }

                return null;
            }
            return null;
        }
        public ServiceEntity UpdateStatus(ServiceEntity ser)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PostAsync(PropertiesFile.HOST + "Service/UpdateStatus/"+ser.idService+"/"+ser.actived, new StringContent(
                    new JavaScriptSerializer().Serialize(ser), Encoding.UTF8, "application/json")).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    return ser;
            }
            return null;
        }
        public List<Models.DTO.ServiceDTO> GetAllByClient(int page)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PropertiesFile.HOST + "Service/Client/GetAll10/" + page);

                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var rs = JsonConvert.DeserializeObject<List<Models.DTO.ServiceDTO>>(result.Content.ReadAsStringAsync().Result);
                    return rs;
                }
                return null;
            }
        }
        public List<ServiceEntity> GetAllByAdmin(int page)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PropertiesFile.HOST + "Service/Admin/GetAll10/" + page);

                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = JsonConvert.DeserializeObject<List<ServiceEntity>>(result.Content.ReadAsStringAsync().Result);
                    return readTask; // nếu return ngay đây sao k return lại method trên luôn
                }
                return null;
            }
        }
        public ServiceEntity GetDetail(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PropertiesFile.HOST + "Service/GetDetail/" + id);

                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = JsonConvert.DeserializeObject<ServiceEntity>(result.Content.ReadAsStringAsync().Result);
                    return readTask; // nếu return ngay đây sao k return lại method trên luôn
                }
                return null;
            }
        }
    }
}