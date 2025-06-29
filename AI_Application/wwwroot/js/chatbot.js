document.addEventListener('DOMContentLoaded', () => {
        fetch("https://openrouter.ai/api/v1/chat/completions", {
        method: "POST",
        headers: {
            "Authorization": "Bearer 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",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            "model": "deepseek/deepseek-r1-0528:free",
            "messages": [
            {
                "role": "user",
                "content": "What is the meaning of life?"
            }
            ]
        })
        }).then(response => {
            if (!response.ok) {
            throw new Error('Network response was not ok');
            }
            return response.json(); // or .text(), .blob(), etc.
        })
        .then(data => {
            console.log(data);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
});