using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using Cassandra.DataStax.Auth;
using Todos.Models;

namespace Todos.Data
{
    public class CassandraTodoContext
    {
        ISession session;
        IMapper mapper;
        public CassandraTodoContext()
        {
            var cluster = Cluster.Builder()
                          .AddContactPoint("127.0.0.1")
                          .Build();
            session = cluster.Connect();
            
            var createKeyspace = new SimpleStatement("CREATE KEYSPACE IF NOT EXISTS todoapikeyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor': '1'};");
            var createTable = new SimpleStatement("CREATE TABLE IF NOT EXISTS todoapikeyspace.todotable (\"Id\" int, \"TaskTitle\" text, \"DateTime\" text, PRIMARY KEY (\"DateTime\", \"TaskTitle\")) WITH CLUSTERING ORDER BY (\"TaskTitle\" ASC);");

            session.Execute(createKeyspace);
            session.Execute(createTable);

            session = cluster.Connect("todoapikeyspace");

            mapper = new Mapper(session);
        }

        public IEnumerable<TodoCassandra> GetElementByDatetime(string dateTime)
        {
            IEnumerable<TodoCassandra> todoItems = mapper.Fetch<TodoCassandra>($"SELECT * FROM todoapikeyspace.todotable WHERE \"DateTime\"='{dateTime}' ORDER BY \"TaskTitle\";");
            session.Dispose();
            return todoItems;
        }

        public void PostTodo(Todo todo)
        {
            var postTodo = new SimpleStatement($"INSERT INTO todoapikeyspace.todotable (\"Id\", \"TaskTitle\", \"DateTime\") VALUES ({todo.Id}, '{todo.TaskTitle}', '{todo.datetime.ToString()}');");
            session.Execute(postTodo);
            session.Dispose();
        }

        public void DeleteTodo(Todo todo)
        {
            var deleteTodo = new SimpleStatement($"DELETE FROM todoapikeyspace.todotable WHERE \"DateTime\"='{todo.datetime.ToString()}' AND \"TaskTitle\"='{todo.TaskTitle}';");
            session.Execute(deleteTodo);
            session.Dispose();
        }

        public void UpdateTodo(DateTime dateTime, Todo todo)
        {
            var postTodo = new SimpleStatement($"INSERT INTO todoapikeyspace.todotable (\"Id\", \"TaskTitle\", \"DateTime\") VALUES ({todo.Id}, '{todo.TaskTitle}', '{dateTime.ToString()}');");
            session.Execute(postTodo);
            session.Dispose();
        }
    }
}
