ALTER TABLE Bookings
ADD CONSTRAINT UC_Booking UNIQUE (BookingDateTime, BookingClientId, BookingCoachId);