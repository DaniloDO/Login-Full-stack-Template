namespace LoginApp.DTOs;

public record AuthResponseDTO(
    string token,
    DateTime expiresAt,
    string userId,
    string email
);
