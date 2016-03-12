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
<br>
<h4><b>
Bước 1: Di chuyển đến thư mục chứa mã nguồn của bạn
</b></h4>
<br>
<code>
cd [path_of_your_project]
</code>
<br>
<h4><b>
Bước 2: Khởi tạo Git repository
</b></h4>
<br>
<code>
git init
</code>
<br>
lệnh khởi tạo này sẽ tạo ra một thư mục ẩn có tên “.git” tại thư mục hiện tại. Đây là thư mục điều khiển của một git repository
<br>
<h4><b>
Bước 3: Đặt các file, thư mục bạn muốn đẩy lên vào repository
</b></h4>
<code>
git add [file_name]
</code>
<br>
bạn có thể add tất cả các file và thư mục có trong project vào repository bằng cách sử dụng lệnh:
<br>
<code>
git add * 
#hoặc 
git add .
</code>
<h4><b>
Bước 4: Lưu tất cả những thay đổi trong mã nguồn vào repository trên máy tính của bạn
</b></h4>
<br>
<code>
git commit -am "message"
</code>
<br>
Ở đây tham số “-am” là lưu tất cả thay đổi. Trong Git phần message ở cuối mỗi lệnh commit là phần bắt buộc.
<br>
<h4><b>
Bước 5: kết nối với repository trên server
</b></h4>
<br>
<code>
git remote add origin git@gitserver:/[username]/[repository].git<br>
# gitserver - là domain name của Git Server. Ví dụ như github.com, bitbucket.org, …
# username - tên đăng nhập của bạn trên Git Server
# repository - tên repository mà bạn đã tạo
</code>
<br>
Lệnh này gồm 2 phần:
<br>
- tạo ra 1 đối tượng “origin” đại diện cho đường dẫn git@gitserver:/[username]/[repository].git bằng lệnh “add”
<br>
- kết nối với đường dẫn git@gitserver:/[username]/[repository].git bằng lệnh “remote”
<br>
Mở file .git/config ra bạn sẽ thấy lệnh này được thể hiện dưới dạng:
<br>
<code>
[remote "origin"]
<br>
    url = git@git.appota.com:thohd/demo.git
<br>
    fetch = +refs/heads/*:refs/remotes/origin/*
<br>
</code>
<br>
<h4><b>
Bước 6: tạo nhánh trong repository để đẩy source code
</b></h4>
<br>
<code>
git branch [tên nhánh]
<br>
# tạo nhánh mới
<br>
git checkout [tên nhánh]
<br>
# di chuyển tới một nhánh
</code>
<br>
tạo ra một nhánh con:
<br>
<code>
git checkout [tên nhánh cha]
<br>
#di chuyển tới tới nhánh muốn tạo nhánh con
<br>
git branch [tên nhánh con]
<br>
# tạo nhánh con
<br>
git merge [tên nhánh con]
<br>
# nối nhánh con vào nhánh cha
</code>
<br>
một nhánh sẽ được lưu trong file .git/config dưới dạng
<br>
<code>
[branch "tên_nhánh"]
    remote = origin
    merge = refs/heads/[tên_nhánh]
</code>
<br>
Bằng cách sử dụng linh hoạt nhánh này bạn có thể tạo ra các phiên bản khác nhau của source code. Ví dụ: project demo, với các version v1, v2, v3, ….
<br>
<code>
source
    v1
        v1.0
        v1.1
        v1.2
    v2
        v2.0
        v2.1
        v2.2
    v3
        v3.0
        v3.1
</code>
<h4><b>Bước 7: Đẩy source code lên server</b></h4>
<code>
git push -u origin [tên nhánh]
# đẩy mã nguồn lên vào một nhánh trên repository git@gitserver:/[username]/[repository].git.
</code>
# đẩy mã nguồn lên vào một nhánh trên repository git@gitserver:/[username]/[repository].git.
# đẩy mã nguồn lên vào một nhánh trên repository git@gitserver:/[username]/[repository].git.
# đẩy mã nguồn lên vào một nhánh trên repository git@gitserver:/[username]/[repository].git.
# TEsdfsd sdf d sdf sfas
# sdf safd safs dss
