using System;
using System.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using tg_bot.response;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tg_bot
{
    public class botx4150
    {
        protected string mTokenID;
        protected User mCurrentUser;
        protected Thread mUpdateThread;
        protected List<services> mServices;

        public User CurrentUser
        { get { return mCurrentUser; } }

        public botx4150(string token_id)
        {
            this.mTokenID = token_id;
            this.mCurrentUser = SendRequest<User>(Methods.getMe);

            if (mCurrentUser.ok)
            {                
                mUpdateThread = new Thread(delegate()
                    {
                        while(true)
                        {
                            HandleRequest();
                        }
                    });
                mServices = new List<services>();
                mServices.Add(new service.getimg(this));
                mServices.Add(new service.latest(this));
            }
        }

        public bool Start()
        {
            if (!mCurrentUser.ok)
                return false;
            mUpdateThread.Start();
            return true;
        }

        int update_id = 1;
        private void HandleRequest()
        {
            var updates = SendRequest<Updates>(
                Methods.getUpdates,
                new Dictionary<string, object>
                {
                    {"offset", update_id},
                    {"limit", 10},
                    {"timeout", 0},
                });
            
            if (updates.ok)
            {
                for (int i = 0; i < updates.result.Length; i++)
                {
                    var update = updates.result[i];
                    update_id = update.update_id + 1;
                    ProcessMessage(update.message.chat.id, update.message.text);
                }
            }
        }

        private void ProcessMessage(int id, string message)
        {
            if (message == null)
                return;
            if (message == "/help")
            {
                var sb = new StringBuilder();
                sb.AppendLine("This bot can do whatever it is progammed for!");
                foreach(var ser in mServices)
                    sb.AppendLine(string.Format("{0}:\t{1}", ser.Caller, ser.Description));
                SendMessage(id, sb.ToString());
            }
            else
            {
                var args = message.Split(' ');
                foreach(var ser in mServices)
                {
                    if (args[0] == ser.Caller)
                    {
                        ser.Execute(id, args);
                        break;
                    }
                }
            }
        }

        public bool SendMessage(int id, string message)
        {
            var response = SendRequest<MessageResponse>(
                Methods.sendMessage,
                new Dictionary<string, object>
                {
                    {"chat_id", id},
                    {"text", message}
                });
            return response.ok;
        }

        public bool SendPhoto(int id, FileToSend aFS)
        {
            var response = SendRequest<MessageResponse>(
                Methods.sendPhoto,
                new Dictionary<string, object>
                {
                    {"chat_id", id},
                    {"photo", aFS}
                });
            return response.ok;
        }

        private T SendRequest<T>(Methods MethodName, Dictionary<string, object> parameters = null)
        {
            return ExecuteMethod<T>(MethodName.ToString(), parameters).Result;
        }
        
        public async Task<T> ExecuteMethod<T>(string MethodName, Dictionary<string, object> parameters = null, bool qualified = false)
        {
            const string BaseUrl = "https://api.telegram.org/bot";

            Uri uri = null;
            if (qualified)
                uri = new Uri(MethodName);
            else
                uri = new Uri(BaseUrl + mTokenID + "/" + MethodName);

            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;
                    if (parameters != null)
                    {
                        using (var form = new MultipartFormDataContent())
                        {
                            foreach (var parameter in parameters)
                            {
                                var content = ConvertParameterValue(parameter.Value);
                                if (parameter.Value is FileToSend)
                                    form.Add(content, parameter.Key, ((FileToSend)parameter.Value).Filename);
                                else
                                    form.Add(content, parameter.Key);
                            }
                            response = await client.PostAsync(uri, form);
                        }                        
                    }
                    else
                    {
                        response = await client.GetAsync(uri);
                    }
                    
                    string json_data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json_data);
                }
                catch (Exception) { }
            }
            throw new Exception("Something went wrong!");
        }

        private static HttpContent ConvertParameterValue(object value)
        {
            var type = value.GetType();

            switch (type.Name)
            {
                case "String":
                case "Int32":
                    return new StringContent(value.ToString());
                case "FileToSend":
                    return new StreamContent(((FileToSend)value).Content);
                case "Boolean":
                    return new StringContent((bool)value ? "true" : "false");
                default:
                    return new StringContent(JsonConvert.SerializeObject(value));
            }
        }
    }
}
