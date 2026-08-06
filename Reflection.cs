//Chip Year = 4,  (15 mod 4) + 1 = 4
// Chip Level = Final, Because Chip Year equals 4, so the label is Final instead of Year 4
//The same problem: validation logic on Range was written on the Student model, If i needed the same range check in 10 places, I'd copy it 10 times, The fix was centralizing it in one place
//All three things (route constraint, validation attribute, tag helper) follow the same pattern: we write a class that the framework calls into at a specific point
//Route constraints run during routing, validation attributes run during model binding, and tag helpers run during view rendering 
//The Gpa badge contains a rule, it decides which Gpa is first class honours 
//The Year chip is just a label, it just displays the year number with colors, no decisions
//I'd be more nervous about duplicating the Gpa badge
//If the first class Gpa threshold changes (3.7 becomes 3.8), I'd have to find every copy of that logic
//Yes, a page could show a card and a row for the same student


//Part E
//The page displayed an empty area where the chip should have appeared because the tag helper was not loaded
//No visible change occurred because the tag helper was already rendering correctly
//The original tag name no longer matched the tag helper, so the helper was not executed