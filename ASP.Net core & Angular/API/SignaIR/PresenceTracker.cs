using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignaIR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnlineUsers
        = new Dictionary<string, List<string>>();
        public  Task<bool> UserConnected(string username, string connectionId)
        {
            bool isOnline = false;
            //kiểm tra UserOnline
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(username))
                {
                    OnlineUsers[username].Add(connectionId);
                }
                else //add Key for UserOnline
                {
                    OnlineUsers.Add(username, new List<string> { connectionId });
                    isOnline=true;
                }
            }
            return Task.FromResult(isOnline);
        }
        public Task<bool> UserDisconnected(string username, string connectionId)
        {
            bool isOffline=false;
            //Lock là khoá kiểm tra nó sẽ chờ, chặn, cho đến khi đối tượng được giải phóng.
            //Thời gian chờ là vô hạn
            lock (OnlineUsers)
            //Nếu UserOnline khác vs ContainsKey thì Offline
                if (!OnlineUsers.ContainsKey(username)) return Task.FromResult(isOffline);
            OnlineUsers[username].Remove(connectionId);
            if (OnlineUsers[username].Count == 0) //kiểm tra connectionId và remove Username
            {
                OnlineUsers.Remove(username);
                isOffline=true;
            }
            return Task.FromResult(isOffline);
        }
        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }
            return Task.FromResult(onlineUsers);
        }
        public Task<List<string>> GetConnectionForUser(string username){
            List<string> connectionIds;
            lock(OnlineUsers)
            {
                //lấy giá trị liên kết trong từ khoá chỉ định
                connectionIds=OnlineUsers.GetValueOrDefault(username);
            }
            return Task.FromResult(connectionIds);
        }
    }
}