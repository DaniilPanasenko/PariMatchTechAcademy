using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Task3
{
    public class AuthenticaionServiceTesting
    {
        private ConcurrentQueue<User> _users;

        private LoginClient _loginClient;

        private int _success;

        private int _failed;

        public AuthenticaionServiceTesting(List<User> users)
        {
            _users = new ConcurrentQueue<User>(users);
            _loginClient = new LoginClient();
            _success = 0;
            _failed = 0;
        }

        private void LoginTesting()
        {
            User user;
            while(_users.TryDequeue(out user))
            {
                if (_loginClient.Login(user.Login, user.Password) == null)
                {
                    Interlocked.Increment(ref _failed);
                }
                else
                {
                    Interlocked.Increment(ref _success);
                }
            }
        }

        public Result Run(int threadCount)
        { 
            CountdownEvent countdownEvent = new CountdownEvent(threadCount);
            for (int i=0;i<threadCount; i++)
            {
                Thread thread = new Thread(() => {
                    LoginTesting();
                    countdownEvent.Signal();
                });
                thread.Start();
            }
            countdownEvent.Wait();
            return new Result(_success, _failed);
        }
    }
}
