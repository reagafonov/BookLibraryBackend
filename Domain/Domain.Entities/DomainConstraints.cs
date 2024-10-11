namespace Domain.Entities;

public static class DomainConstraints
{
    public static int BookTitleMaxLength => 500;
    public static int BookDescriptionMaxLength => 2000;
    public static bool BookDescriptionIsRequired => false;
    public static bool BookTitleIsRequired => true;

    public static bool IsTitleUnique => true;
    public static bool BookMainAuthorIdIsRequired => true;

    public static int AuthorFirstNameMaxLength => 500;
    public static bool AuthorFirstNameIsRequired => true;
    public static int AuthorLastNameMaxLength => 500;
    public static bool AuthorLastNameIsRequired => true;
    public static bool IsCheckForSameLanguageFirstNameAndLastName => true;
}