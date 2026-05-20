let cart = [];

function updateCartUI() {
    const container = document.getElementById('cart-items');
    const subtotalEl = document.getElementById('cart-total');
    const finalTotalEl = document.getElementById('final-total');
    const submitBtn = document.getElementById('submitOrderBtn');
    const emptyMsg = document.querySelector('.empty-cart-msg');
    const discountSelect = document.getElementById('discount-select');

    container.innerHTML = '';
    let subtotal = 0;

    if (cart.length === 0) {
        container.innerHTML = '<p class="text-muted text-center empty-cart-msg">Chưa có món nào.</p>';
        submitBtn.disabled = true;
    } else {
        submitBtn.disabled = false;

        cart.forEach((item, index) => {
            const itemTotal = item.price * item.quantity;
            subtotal += itemTotal;

            const itemHtml = `
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <div>
                                <h6 class="my-0">${item.name}</h6>
                                <small class="text-muted">${item.price.toLocaleString()} x ${item.quantity}</small>
                            </div>
                            <div class="d-flex align-items-center">
                                <span class="mr-3">${itemTotal.toLocaleString()} đ</span>
                                <button type="button" class="btn btn-sm btn-danger px-2 py-1 remove-item-btn" data-index="${index}">
                                    <i class="fa fa-times"></i>
                                </button>
                            </div>
                            <!-- Hidden inputs to submit to ASP.NET Core -->
                            <input type="hidden" name="Input.OrderDetails[${index}].ProductId" value="${item.id}" />
                            <input type="hidden" name="Input.OrderDetails[${index}].Quantity" value="${item.quantity}" />
                        </div>
                    `;
            container.insertAdjacentHTML('beforeend', itemHtml);
        });
    }

    subtotalEl.innerText = subtotal.toLocaleString() + ' VNĐ';

    // Calculate Final Total
    let finalTotal = subtotal;
    if (discountSelect && discountSelect.selectedIndex > 0) {
        const option = discountSelect.options[discountSelect.selectedIndex];
        const type = option.getAttribute('data-type').toLowerCase();
        const value = parseFloat(option.getAttribute('data-value'));

        if (type === 'flat') {
            finalTotal -= value;
        } else if (type === 'percent') {
            finalTotal -= subtotal * (value);
        }
    }

    if (finalTotal < 0) finalTotal = 0;
    if (finalTotalEl) {
        finalTotalEl.innerText = finalTotal.toLocaleString() + ' VNĐ';
    }

    // Bind remove events
    document.querySelectorAll('.remove-item-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const idx = parseInt(this.getAttribute('data-index'));
            cart.splice(idx, 1);
            updateCartUI();
        });
    });
}

document.querySelectorAll('.add-to-cart-btn').forEach(btn => {
    btn.addEventListener('click', function () {
        const id = parseInt(this.getAttribute('data-id'));
        const name = this.getAttribute('data-name');
        const price = parseFloat(this.getAttribute('data-price'));

        const existingItem = cart.find(x => x.id === id);
        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            cart.push({ id, name, price, quantity: 1 });
        }

        updateCartUI();
    });
});