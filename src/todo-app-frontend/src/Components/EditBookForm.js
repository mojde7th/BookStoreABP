import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getBook, updateBook } from '../services/bookService';
import { getPublishers } from '../services/publisherService';

const EditBookForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [book, setBook] = useState({ name: '', author: '', price: 0, publisherId: '' });
  const [publishers, setPublishers] = useState([]);

  useEffect(() => {
    getPublishers().then(response => setPublishers(response.data));
    getBook(id).then(response => setBook(response.data));
  }, [id]);

  const handleChange = e => {
    const { name, value } = e.target;
    setBook(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = e => {
    e.preventDefault();
    updateBook(id, book)
      .then(() => navigate('/books'))
      .catch(error => console.error('Error updating book:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={book.name} onChange={handleChange} required />
      </div>
      <div>
        <label>Author</label>
        <input type="text" name="author" value={book.author} onChange={handleChange} required />
      </div>
      <div>
        <label>Price</label>
        <input type="number" name="price" value={book.price} onChange={handleChange} required />
      </div>
      <div>
        <label>Publisher</label>
        <select name="publisherId" value={book.publisherId} onChange={handleChange} required>
          <option value="">Select Publisher</option>
          {publishers.map(publisher => (
            <option key={publisher.id} value={publisher.id}>{publisher.name}</option>
          ))}
        </select>
      </div>
      <button type="submit">Update</button>
    </form>
  );
};

export default EditBookForm;
