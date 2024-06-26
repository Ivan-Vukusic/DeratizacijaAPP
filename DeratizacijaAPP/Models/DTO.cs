﻿using DeratizacijaAPP.Validations;
using System.ComponentModel.DataAnnotations;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// DTO read za djelatnika
    /// </summary>
    /// <param name="sifra"></param>
    /// <param name="ime"></param>
    /// <param name="prezime"></param>
    /// <param name="brojMobitela"></param>
    /// <param name="oib"></param>
    /// <param name="struka"></param>
    /// <param name="slika"></param>
    public record DjelatnikDTORead(int sifra, string? ime, string? prezime, string? brojMobitela, string? oib, string? struka, string? slika);

    /// <summary>
    /// DTO insert/update za djelatnika
    /// </summary>
    /// <param name="ime"></param>
    /// <param name="prezime"></param>
    /// <param name="brojMobitela"></param>
    /// <param name="oib"></param>
    /// <param name="struka"></param>
    /// <param name="slika"></param>
    public record DjelatnikDTOInsertUpdate(
       
        [Required(ErrorMessage = "Ime obavezno")]
        string? ime,

        [Required(ErrorMessage = "Prezime obavezno")]
        string? prezime,

        string? brojMobitela,

        [StringLength(11, MinimumLength = 11, ErrorMessage ="Neispravan unos! OIB mora imati 11 znamenki")]
        [OibValidator]
        string? oib,

        string? struka,
        string? slika);

    /// <summary>
    /// DTO read za otrov
    /// </summary>
    /// <param name="sifra"></param>
    /// <param name="naziv"></param>
    /// <param name="aktivnaTvar"></param>
    /// <param name="kolicina"></param>
    /// <param name="casBroj"></param>
    public record OtrovDTORead(int sifra, string? naziv, string? aktivnaTvar, decimal? kolicina, string? casBroj);

    /// <summary>
    /// DTO insert/update za otrov
    /// </summary>
    /// <param name="naziv"></param>
    /// <param name="aktivnaTvar"></param>
    /// <param name="kolicina"></param>
    /// <param name="casBroj"></param>
    public record OtrovDTOInsertUpdate(
       
        [Required(ErrorMessage = "Naziv obavezan")]
        string? naziv,

        [Required(ErrorMessage = "Aktivna tvar obavezna")]
        string? aktivnaTvar,
        
        decimal? kolicina,
        string? casBroj);

    /// <summary>
    /// DTO read za vrstu
    /// </summary>
    /// <param name="sifra"></param>
    /// <param name="naziv"></param>
    public record VrstaDTORead(int sifra, string? naziv);

    /// <summary>
    /// DTO insert/update za vrstu
    /// </summary>
    /// <param name="naziv"></param>
    public record VrstaDTOInsertUpdate(

        [Required(ErrorMessage = "Naziv obavezno")]
        string? naziv);

    /// <summary>
    /// DTO read za objekt
    /// </summary>
    /// <param name="sifra"></param>
    /// <param name="mjesto"></param>
    /// <param name="adresa"></param>
    /// <param name="vrstaNaziv"></param>    
    public record ObjektDTORead(int sifra, string? mjesto, string? adresa, string? vrstaNaziv);

    /// <summary>
    /// DTO insert/update za objekt
    /// </summary>
    /// <param name="mjesto"></param>
    /// <param name="adresa"></param>
    /// <param name="vrstaSifra"></param>
    public record ObjektDTOInsertUpdate(

        [Required(ErrorMessage = "Mjesto obavezno")]
        string? mjesto,

        [Required(ErrorMessage = "Adresa obavezna")]
        string? adresa, 

        int? vrstaSifra);

    /// <summary>
    /// DTO read za termin
    /// </summary>
    /// <param name="sifra"></param>
    /// <param name="datum"></param>
    /// <param name="djelatnikImePrezime"></param>
    /// <param name="objektMjestoAdresa"></param>
    /// <param name="otrovNaziv"></param>
    /// <param name="napomena"></param>
    public record TerminDTORead(int sifra, DateTime? datum, string? djelatnikImePrezime, 
        string? objektMjestoAdresa, string? otrovNaziv, string? napomena);

    /// <summary>
    /// DTO insert/update za termin
    /// </summary>
    /// <param name="datum"></param>
    /// <param name="djelatnikSifra"></param>
    /// <param name="objektSifra"></param>
    /// <param name="otrovSifra"></param>
    /// <param name="napomena"></param>
    public record TerminDTOInsertUpdate(

        [Required(ErrorMessage = "Datum obavezan")]
        DateTime? datum,

        int? djelatnikSifra,
        int? objektSifra, 
        int? otrovSifra, 
        string? napomena);

    /// <summary>
    /// Operater
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public record OperaterDTO(
        [Required(ErrorMessage = "Email obavezno")]
        string? email,
        [Required(ErrorMessage = "Lozinka obavezno")]
        string? password);

    /// <summary>
    /// Slika
    /// </summary>
    /// <param name="Base64"></param>
    public record SlikaDTO([Required(ErrorMessage = "Base64 zapis slike obavezno")] string Base64);
}
