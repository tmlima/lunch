using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using Lunch.Domain.Services;
using Lunch.Infra.Data.Data;
using Lunch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace Lunch.Test.BDD.Steps
{
    [Binding]
    public class Estoria1Steps
    {
        private readonly ScenarioContext _scenarioContext;
        private IPoolService poolAppService;
        private IRestaurantService restaurantAppService;
        private IUserService userAppService;

        public Estoria1Steps( ScenarioContext scenarioContext )
        {
            _scenarioContext = scenarioContext;

            string dbName = Guid.NewGuid().ToString();
            DbContextOptionsBuilder<LunchDbContext> contextOptionsBuilder = new DbContextOptionsBuilder<LunchDbContext>().UseInMemoryDatabase( dbName );
            LunchDbContextInMemory dbContext = new LunchDbContextInMemory( contextOptionsBuilder.Options );

            IRestaurantRepository restaurantRepository = new RestaurantRepository( dbContext );
            IUserRepository userRepository = new UserRepository( dbContext );
            IVoteRepository voteRepository = new VoteRepository( dbContext );
            IPoolRepository poolRepository = new PoolRepository( dbContext );

            restaurantAppService = new RestaurantService( restaurantRepository );
            userAppService = new UserService( userRepository );
            poolAppService = new PoolService( poolRepository, voteRepository, restaurantAppService, userAppService );
        }

        [Given( @"uma eleição" )]
        public void GivenUmaEleicao()
        {
            int poolId = poolAppService.CreatePool( DateTime.Now.AddHours( 1 ) );
            _scenarioContext.Add( "poolId", poolId );
        }

        [Given( @"um usuário" )]
        public void GivenUmUsuario()
        {
            int userId = userAppService.CreateUser( "first" );
            _scenarioContext.Add( "userId", userId );
        }

        [Given( @"um restaurante ""(.*)""" )]
        public void GivenUmRestaurante( string restaurante )
        {
            int restaurantId = restaurantAppService.Add( restaurante );
            _scenarioContext.Add( "restaurantId" + restaurante, restaurantId );
        }

        [Given( @"eu não tenha votado" )]
        public void GivenEuNaoTenhaVotado()
        {
            int poolId = _scenarioContext.Get<int>( "poolId" );
            Dictionary<Restaurant, int> results = poolAppService.GetResults( poolId );
            Assert.Empty( results.Keys );
        }

        [Given( @"eu tenha votado no restaurante ""(.*)""" )]
        public void GivenEuTenhaVotadoNoRestaurante( string restaurante )
        {
            int poolId = _scenarioContext.Get<int>( "poolId" );
            int userId = _scenarioContext.Get<int>( "userId" );
            int restaurantId = _scenarioContext.Get<int>( "restaurantId" + restaurante );
            poolAppService.Vote( poolId, userId, restaurantId );
        }

        [Given( @"eleição tenha sido encerrada" )]
        public void GivenUmaEleicaoQueJaTenhaSidoEncerrada()
        {
            TimeSpan oneSecond = TimeSpan.FromSeconds( 1 );
            int poolId = poolAppService.CreatePool( DateTime.Now.Add( oneSecond ) );
            Thread.Sleep( oneSecond );
            _scenarioContext.Set<int>( poolId, "poolId" );
        }

        [When( @"eu votar no restaurante ""(.*)""" )]
        public void WhenEuVotarNoRestaurante( string restaurante )
        {
            int userId = _scenarioContext.Get<int>( "userId" );
            int poolId = _scenarioContext.Get<int>( "poolId" );
            int restaurantId = restaurantAppService.GetByName( restaurante ).Id;

            IReadOnlyCollection<string> errors = poolAppService.CanVote( poolId, userId );
            if ( errors.Any() )
            {
                foreach ( string e in errors )
                    _scenarioContext.Add( "error", e );
            }
            else
            {
                poolAppService.Vote( poolId, userId, restaurantId );
            }
        }

        [Then( @"vai aparecer nos resultados somente um voto" )]
        public void ThenVaiAparecerNosResultadosSomenteUmVoto()
        {
            int poolId = _scenarioContext.Get<int>( "poolId" );
            Dictionary<Restaurant, int> results = poolAppService.GetResults( poolId );
            Assert.Equal( 1, results.Sum( x => x.Value ) );
        }

        [Then( @"vai aparecer a mensagem de erro ""(.*)""" )]
        public void ThenVaiAparecerAMensagemDeErro( string message )
        {
            string error = _scenarioContext.Get<string>( "error" );
            Assert.Equal( message, error );
        }
    }
}
