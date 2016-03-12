Sau khi tạo được kết nối với Git Server, điều bạn quan tâm lớn nhất chắc hẳn là làm thế nào để đẩy source code lên Git Server.
<br>
Bài viết này sẽ hướng dẫn đẩy source code lên Git Server bằng cách sử dụng git-shell (sử dụng dòng lệnh).
<br>
<h2>Tạo repository trên Git Server</h2>
<br>
Để đẩy source code lên Git Server, trước hết bạn phải tạo ra một repository trên server để lưu trữ source code. Một repository cũng giống như thư mục gốc của một project, nơi bạn có thể lưu trữ source code, các phiên bản khác nhau của dự án, viết tài liệu hướng dẫn,…
<br>
Một repository có thể tùy biến rất linh động để đáp ứng cho những quy mô dự án khác nhau và phù hợp với những phong cách quản lí dự án khác nhau.
<br>

Khung taj repository trên GitHub
<br>
Sau khi tạo ra một repository bạn có thể tiến hành đẩy mã nguồn của mình lên Git Server.

Đẩy mã nguồn lên Git Server

Bước 1: Di chuyển đến thư mục chứa mã nguồn của bạn
<code>
cd [path_of_your_project]
</code>
Bước 2: Khởi tạo Git repository
<code>
git init
</code>
lệnh khởi tạo này sẽ tạo ra một thư mục ẩn có tên “.git” tại thư mục hiện tại. Đây là thư mục điều khiển của một git repository
