    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using FluentValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using TransactionService.Application.DTOs.Requests;
    using TransactionService.Application.Usecases;
    using TransactionService.Application.UseCases;

    namespace TransactionService.API.Controllers
    {
        [Route("api/transactions")]
        [ApiController]
        public class TransactionController : ControllerBase
        {
            private readonly CreateTransactionHandler _createTransactionHandler;
            private readonly GetPaymentHandler _getPaymentHandler;
            private readonly GetTransactionHandler _getTransactionsHandler;
            private readonly GetDetailTransactionHandler _getDetailTransactionHandler;
            private readonly PayTransactionHandler _payTransactionHandler;
            private readonly IValidator<TransactionRequest> _ticketRequestValidator;
            private readonly IValidator<TransactionQueryParams> _ticketQueryParamsValidator;
            private readonly IValidator<TransactionPaymentRequest> _ticketPaymentRequestValidator;

            public TransactionController(
                CreateTransactionHandler createTransactionHandler,
                GetTransactionHandler getTransactionsHandler,
                GetPaymentHandler getPaymentHandler,
                GetDetailTransactionHandler getDetailTransactionHandler,
                PayTransactionHandler payTransactionHandler,
                IValidator<TransactionRequest> ticketRequestValidator,
                IValidator<TransactionQueryParams> ticketQueryParamsValidator,
                IValidator<TransactionPaymentRequest> ticketPaymentRequestValidator
            )
            {
                _getPaymentHandler = getPaymentHandler;
                _createTransactionHandler = createTransactionHandler;
                _getTransactionsHandler = getTransactionsHandler;
                _getDetailTransactionHandler = getDetailTransactionHandler;
                _ticketRequestValidator = ticketRequestValidator;
                _ticketQueryParamsValidator = ticketQueryParamsValidator;
                _ticketPaymentRequestValidator = ticketPaymentRequestValidator;
                _payTransactionHandler = payTransactionHandler;
            }

            [HttpGet]
            [Route("payment-methods")]
            public async Task<IActionResult> Get()
            {
                var result = await _getPaymentHandler.Handle();
                return Ok(result);
            }

            [HttpPost]
            [Authorize(Roles = "User,Admin")]
            public async Task<IActionResult> Create([FromBody] TransactionRequest request)
            {
                await _ticketRequestValidator.ValidateAsync(request);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _createTransactionHandler.Handle(int.Parse(userId), request);
                return CreatedAtAction(nameof(Create), result);
            }

            [HttpGet]
            [Authorize(Roles = "User,Admin")]
            public async Task<IActionResult> GetTransactions([FromQuery] TransactionQueryParams queryParams)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _ticketQueryParamsValidator.ValidateAsync(queryParams);
                var result = await _getTransactionsHandler.Handle(int.Parse(userId), queryParams);
                return Ok(result);
            }

            [HttpGet]
            [Authorize(Roles = "User,Admin")]
            [Route("{id}")]
            public async Task<IActionResult> GetDetailTransactions([FromRoute] int id)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _getDetailTransactionHandler.Handle(int.Parse(userId), id);
                return Ok(result);
            }

            [HttpPut]
            [Authorize(Roles = "User,Admin")]
            [Route("{id}/payment")]
            public async Task<IActionResult> PayTransaction([FromRoute] int id, [FromBody] TransactionPaymentRequest request)
            {
                await _ticketPaymentRequestValidator.ValidateAsync(request);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _payTransactionHandler.Handle(int.Parse(userId), id, request);
                return Ok(result);
            }
        }
    }