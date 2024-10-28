let altBenefCpf;
let altBenefNome;

$(document).ready(function () {
    $('.AltBenefCpf').mask('000.000.000-00');
    $('#AddBenefCpf').mask('000.000.000-00');

    $('#beneficiariosModal').on('show.bs.modal', function () {
        carregarBeneficiarios();
    });
    
    $('#btnAddBenef').on('click', function (e) {
        var cpf = $('#AddBenefCpf').val().trim();
        var nome = $('#AddBenefNome').val().trim();

        if (!cpf || !nome) {
            return;
        }

        e.preventDefault();

        const novoBeneficiario = {
            CPF: cpf,
            Nome: nome,
            IdCliente: idCliente
        };

        $.ajax({
            url: urlBeneficiarioIncluir,
            method: 'POST',
            data: JSON.stringify(novoBeneficiario),
            contentType: 'application/json',
            success: function (response) {
                ModalDialog('Sucesso', response);

                $('#AddBenefCpf').val('');
                $('#AddBenefNome').val('');

                carregarBeneficiarios();
            },
            error: function (e) {
                ModalDialog('Erro', e.responseJSON);
            }
        });
    });

    $('#gridBeneficiarios').on('click', '.btn-alterar', function () {
 
        $('#AddBenefCpf, #AddBenefNome, #btnAddBenef').prop('disabled', true);

        const $row = $(this).closest('tr');

        originalCpf = $row.find('.AltBenefCpf').val();
        originalNome = $row.find('.AltBenefNome').val();

        $('#gridBeneficiarios .btn-alterar, #gridBeneficiarios .btn-excluir').prop('disabled', true);
        
        $row.find('.AltBenefCpf, .AltBenefNome').prop('disabled', false);
        //$row.find('.AltBenefNome, input[type="text"]').prop('disabled', false);
        
        $row.find('.btn-alterar, .btn-excluir').hide();
        
        $row.find('.btn-confirmar, .btn-cancelar').show();
    });
    $('#gridBeneficiarios').on('click', '.btn-cancelar', function () {
        const $row = $(this).closest('tr');

        $('#CPFBenefAdd, #NomeBeneficiario, #btnAddBeneficiario').prop('disabled', false);

        $row.find('.AltBenefCpf').val(originalCpf);
        $row.find('.AltBenefNome').val(originalNome);

        $('#gridBeneficiarios .btn-alterar, #gridBeneficiarios .btn-excluir').prop('disabled', false);

        $row.find('.AltBenefCpf, .AltBenefNome').prop('disabled', true);

        $row.find('.btn-alterar, .btn-excluir').show();

        $row.find('.btn-confirmar, .btn-cancelar').hide();

        $('#AddBenefCpf, #AddBenefNome, #btnAddBenef').prop('disabled', false);
    });

    $(document).on('click', '.btn-confirmar', function () {
        const linha = $(this).closest('tr');
        const idBeneficiario = $(this).data('id');
        const cpf = linha.find('.AltBenefCpf').val();
        const nome = linha.find('.AltBenefNome').val();

        const beneficiarioAtualizado = {
            Id: idBeneficiario,
            CPF: cpf,
            Nome: nome,
            idCliente: idCliente
        };

        $.ajax({
            url: urlBeneficiarioAtualizar,
            method: 'POST',
            data: JSON.stringify(beneficiarioAtualizado),
            contentType: 'application/json',
            success: function (response) {
                ModalDialog('Sucesso', response);

                linha.find('.AltBenefCpf, .AltBenefNome').prop('disabled', true);
                linha.find('.btn-confirmar, .btn-cancelar').hide();
                linha.find('.btn-alterar, .btn-excluir').show();

                $('#AddBenefCpf, #AddBenefNome, #btnAddBenef').prop('disabled', false);

                $('#gridBeneficiarios .btn-alterar').prop('disabled', false);
                $('#gridBeneficiarios .btn-excluir').prop('disabled', false);
            },
            error: function (e) {
                ModalDialog('Erro', e.responseJSON);
            }
        });
    });

    $('#gridBeneficiarios').on('click', '.btn-excluir', function () {
        const idBeneficiario = $(this).data('id');

        console.log($(this));
        console.log(idBeneficiario);
        const beneficiarioDeletar = {
            Id: idBeneficiario
        };

        console.log(beneficiarioDeletar);

        $.ajax({
            url: urlBeneficiarioExcluir,
            method: 'POST',
            data: JSON.stringify(beneficiarioDeletar),
            contentType: 'application/json',
            success: function (response) {
                ModalDialog('Sucesso!', response);
                carregarBeneficiarios();
            },
            error: function (e) {
                ModalDialog('Erro!', e);
            }
        });
    });
    $('#beneficiariosModal').on('hidden.bs.modal', function () {

        $('#AddBenefCpf').val('');
        $('#AddBenefNome').val('');

        $('#AddBenefCpf').prop('disabled', false)
        $('#AddBenefNome').prop('disabled', false)

        $('#gridBeneficiarios .btn-alterar').prop('disabled', false);
        $('#gridBeneficiarios .btn-excluir').prop('disabled', false);

        $('#gridBeneficiarios .AltBenefCpf, #gridBeneficiarios .AltBenefNome').prop('disabled', true);
        $('#gridBeneficiarios .btn-confirmar, #gridBeneficiarios .btn-cancelar').hide();
    });

});

function carregarBeneficiarios() {
    $.ajax({
        url: urlBeneficiarioList,
        method: 'POST',
        success: function (data) {

            $('#gridBeneficiarios tbody').empty();

            data.forEach(function (benef) {
                let linha = `
                    <tr>
                        <td><input class="AltBenefCpf form-control" value="${benef.CPF}" disabled /></td>
                        <td><input class="AltBenefNome form-control" value="${benef.Nome}" disabled /></td>
                        <td class="d-flex">
                            <button class="btn btn-primary btn-alterar">Alterar</button>
                            <button class="btn btn-danger btn-excluir" data-id="${benef.Id}">Excluir</button>
                            <button type="button" class="btn btn-success btn-confirmar" style="display: none;" data-id="${benef.Id}">Confirmar</button>
                            <button type="button" class="btn btn-secondary btn-cancelar" style="display: none;">Cancelar</button>
                        </td>
                    </tr>
                `;
                $('#gridBeneficiarios tbody').append(linha);
            });
            $('.AltBenefCpf').mask('000.000.000-00');
        },
        error: function () {
            ModalDialog('Erro', 'Nao foi possivel carregar a lista de beneficiarios.');
        }
    });
    
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
